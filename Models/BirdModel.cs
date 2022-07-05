namespace floppy_bird_aliexpres.Models
{
    public class BirdModel
    {
        public int DistanceFromGround { get; set; } = 100;
        public int JumpStength { get; private set; } = 50;
        public void Fall(int gravity)
        {
            DistanceFromGround -= gravity;
        }
        public void Jump()
        {
            if(DistanceFromGround<=530)
                DistanceFromGround += JumpStength;
        }
    }
}
