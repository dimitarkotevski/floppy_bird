

using System.ComponentModel;

namespace floppy_bird_aliexpres.Models
{
    public class GameManager : INotifyPropertyChanged
    {
        private readonly int gravity = 2;

        public event PropertyChangedEventHandler? PropertyChanged;

        public BirdModel Bird { get; set; }
        public bool isRunning { get; set; } = false;
        public GameManager()
        {
            Bird=new BirdModel();
        }
        public async void MainLoop()
        {
            isRunning = true;
            while (isRunning)
            {
                Bird.Fall(gravity);
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Bird)));
                await Task.Delay(20);
            }
        }
    }
}
