using FbonizziMonoGame.Exceptions;
using FbonizziMonoGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FbonizziMonoGame.Achievements
{
    public class AchievementsManager
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly IDictionary<string, AchievementToDraw> _achievements;
        private List<PopupAchievement> _popupAchievements = new List<PopupAchievement>();
        public Rectangle ListObjectBox => _achievements.First().Value.Container;

        public bool SomeAchievementsEarned { get; private set; } = false;

        private const float _achievementPopupSpeed = 40f;
        private readonly TimeSpan _achievementPopupLifeTime = TimeSpan.FromSeconds(3f);

        public int GameCompletionPercentage
            => (int)((_achievements.Count(a => a.Value.AchievementItem.IsCompleted) / (float)_achievements.Count) * 100);

        public Vector2 AchievementPopupsStartingPosition { get; set; } = new Vector2(200f, 200f);

        public AchievementsManager(
            SpriteFont font,
            float fontSize,
            Sprite rewardSprite,
            IDictionary<string, AchievementItem> achievementDefinitions,
            ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository ?? throw new ArgumentNullException(nameof(settingsRepository));

            _achievements = new Dictionary<string, AchievementToDraw>();
            foreach(var achievementDefinition in achievementDefinitions)
            {
                achievementDefinition.Value.CompletedDate =
                    settingsRepository.GetOrSetDateTime(achievementDefinition.Key, default(DateTime));
                _achievements.Add(achievementDefinition.Key, new AchievementToDraw(
                    achievementDefinition.Value,
                    font,
                    fontSize,
                    rewardSprite));
            }
        }

        public void SetCompetionCheckFunction(string achievementKey, Func<bool> checkFunc)
        {
            _achievements[achievementKey].AchievementItem.AchievementCompletionCheck = checkFunc;
        }

        private bool IsNewAchievementEarned(
            string achievementKey,
            Func<bool> checkFunc)
        {
            // Se ce l'ho già, non ripeto nemmeno il check della func
            if (_achievements[achievementKey].AchievementItem.IsCompleted)
                return false;

            if (!checkFunc())
                return false;

            var now = DateTime.Now;
            _achievements[achievementKey].SetCompletionDate(now);
            _settingsRepository.SetDateTime(achievementKey, now);

            return true;
        }

        public void PopupAchievement(
            string achievementKey,
            Vector2 where,
            float upSpeed,
            TimeSpan lifetime)
        {
            var popup = new PopupAchievement()
            {
                Achievement = _achievements[achievementKey],
                PopupObject = new PopupObject(
                    lifetime,
                    where,
                    Color.White,
                    upSpeed)
            };
            popup.PopupObject.Popup();
            _popupAchievements.Add(popup);
        }

        public void Update(TimeSpan elapsed)
        {
            foreach(var achievementsDefinitions in _achievements)
            {
                if (achievementsDefinitions.Value.AchievementItem.AchievementCompletionCheck == null)
                    throw new AchievementCheckFuncNotDefinedException(achievementsDefinitions.Key);

                if (achievementsDefinitions.Value.AchievementItem.AchievementCompletionCheck())
                {
                    if (IsNewAchievementEarned(
                        achievementsDefinitions.Key, 
                        achievementsDefinitions.Value.AchievementItem.AchievementCompletionCheck))
                    {
                        SomeAchievementsEarned = true;
                        PopupAchievement(
                            achievementsDefinitions.Key,
                            AchievementPopupsStartingPosition,
                            _achievementPopupSpeed,
                            _achievementPopupLifeTime);
                    }
                }
            }

            bool somePopupCompleted = false;
            foreach (var popup in _popupAchievements)
            {
                popup.PopupObject.Update(elapsed);
                popup.Achievement.SetPosition(popup.PopupObject.Position);
                popup.Achievement.SetAlpha(popup.PopupObject.Alpha);
                if (popup.PopupObject.IsCompleted)
                    somePopupCompleted = true;
            }

            if (somePopupCompleted)
                _popupAchievements.RemoveAll(p => p.PopupObject.IsCompleted);
        }
        
        public void DrawPopups(SpriteBatch spriteBatch)
        {
            foreach (var popup in _popupAchievements)
                popup.Achievement.Draw(spriteBatch);
        }

        public void DrawList(
            SpriteBatch spriteBatch,
            float startingYPosition,
            float virtualViewPortWidth)
        {
            var startingPosition = new Vector2(10f, startingYPosition);

            // Prima colonna
            foreach (var achievement in _achievements.Take(_achievements.Count / 2))
            {
                achievement.Value.Draw(spriteBatch, startingPosition);
                startingPosition.Y += achievement.Value.Container.Height + 4;
            }

            // Seconda colonna
            startingPosition.X = virtualViewPortWidth - 10f - AchievementToDraw.Width;
            startingPosition.Y = startingYPosition;

            foreach (var achievement in _achievements.Skip(_achievements.Count / 2))
            {
                achievement.Value.Draw(spriteBatch, startingPosition);
                startingPosition.Y += achievement.Value.Container.Height + 4;
            }
        }
    }
}
