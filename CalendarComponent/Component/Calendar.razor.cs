using Microsoft.AspNetCore.Components;
namespace CalendarComponent.Component
{
    public partial class Calendar
    {
        private int CurrentMonth { get; set; }
        private int CurrentYear { get; set; }
        private int SelectedDay { get; set; }
        public DateTime? SelectedDate { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        [Parameter]
        public DateTime? Value { get; set; }
        private bool ShowDialog { get; set; }
        private DateTime FirstDayOfMonth => new DateTime(CurrentYear, CurrentMonth, 1);
        private int DaysInMonth =>
            CurrentMonth switch
            {
                2 => DateTime.IsLeapYear(CurrentYear) ? 29 : 28,
                4 or 6 or 9 or 11 => 30,
                _ => 31,
            };
        protected override void OnInitialized()
        {
            var today = DateTime.Today;
            CurrentMonth = today.Month;
            CurrentYear = today.Year;
            SelectedDay = today.Day;
            if (Value.HasValue)
            {
                CurrentMonth = Value.Value.Month;
                CurrentYear = Value.Value.Year;
                SelectedDay = Value.Value.Day;
                SelectedDate = Value.Value;
            }
        }
        private async void SelectDay(int day)
        {
            SelectedDay = day;
            SelectedDate = new DateTime(CurrentYear, CurrentMonth, SelectedDay);
            await CloseEventCallback.InvokeAsync(true);
            ShowDialog = false;

            StateHasChanged();
        }
        private void PrevMonth()
        {
            CurrentMonth--;
            if (CurrentMonth == 0)
            {
                CurrentMonth = 12;
                CurrentYear--;
            }
        }
        private void NextMonth()
        {
            CurrentMonth++;
            if (CurrentMonth == 13)
            {
                CurrentMonth = 1;
                CurrentYear++;
            }
        }
        public void Show()
        {
            ShowDialog = !ShowDialog;
            StateHasChanged();
        }
        public void Hide()
        {
            ShowDialog = false;
            StateHasChanged();
        }
    }
}
