using System;

namespace FbonizziMonogame.Achievements
{
    public class AchievementItem
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public Func<bool> AchievementCompletionCheck { get; set; }

        private DateTime _completedDate;
        public DateTime CompletedDate
        {
            get => _completedDate;
            set
            {
                _completedDate = value;
                if (_completedDate != default(DateTime))
                    CompletedDateString = _completedDate.ToString("dd/MM/yyyy");
                else
                    CompletedDateString = string.Empty;
            }
        }

        public string CompletedDateString { get; private set; } = string.Empty;
        public bool IsCompleted => _completedDate != default(DateTime);
    }
}
