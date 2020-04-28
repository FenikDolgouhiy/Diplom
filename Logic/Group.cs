namespace Logic
{
    public class Group //массив групп. На будущее
    {
        public string Name;
        public int Weeks;
        public int Practice;
        public Rozklad[,] TimeTable = new Rozklad[5, 6];

        public Group()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                    TimeTable[i, j] = new Rozklad();
            }
        }

        public Group(string N, int W, int P) : this()
        {
            Name = N;
            Weeks = W;
            Practice = P;
        }

    }
}
