

using System.ComponentModel;

namespace floppy_bird_aliexpres.Models
{
    public class GameManager
    {
        private readonly int gravity = 2;

        public event EventHandler MainLoopCompleted;

        public BirdModel Bird { get; set; }
        public List<PipeModel> Pipes { get; private set; }
        public bool isRunning { get; set; } = false;
        public GameManager()
        {
            Bird=new BirdModel();
            Pipes=new List<PipeModel>();
        }
        public async void MainLoop()
        {
            isRunning = true;
            while (isRunning)
            {
                MoveObject();

                CheckForCollision();

                ManagePipe();
                
                MainLoopCompleted?.Invoke(this ,EventArgs.Empty);
                await Task.Delay(20);
            }
        }

        private void ManagePipe()
        {
            if(!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
            {
                Pipes.Add(new PipeModel());
            }
            if(Pipes.First().IsOffScreen())
                Pipes.Remove(Pipes.First());
        }

        public void CheckForCollision()
        {
            if (Bird.DistanceFromGround <= 0)
                GameOver();

            var centeredPipe = Pipes.FirstOrDefault(p => p.isCentered());
            if(centeredPipe != null)
            {
                bool hasCollidedWithBottom = Bird.DistanceFromGround < centeredPipe.GapBottom - 150;
                bool hasCollidedWithTop = Bird.DistanceFromGround+45 > centeredPipe.GapTop - 150;
                if(hasCollidedWithBottom || hasCollidedWithTop)
                {
                    GameOver();
                }
            }
        }
        
        public void MoveObject()
        {
            Bird.Fall(gravity);
            foreach (var Pipe in Pipes)
                Pipe.Move();
        }

        public void Jump()
        {
            if (isRunning)
            {
                Bird.Jump();
            }
        }

        private void GameOver()
        {
            isRunning = false;
        }

        public void StartGame()
        {
            if (!isRunning)
            {
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }
        }
    }
}
