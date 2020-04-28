namespace Logic
{
    public class TeacherList //список преподов
    {
        public int subjectsCount; //количество предметов       
        public string teacherName; //имя препода   
        public bool[,] opportunities = new bool[5, 6]; //матрица условий
        public Subjects[] subjects; //массив предметов
        public int possibilitiesCount = 0;   //суммарное количество возможностей. Для сортировки
        public bool[,,] TOpp = new bool[5, 6, 2];
        public TeacherList(int subjectsCount, string teacherName, string opportunitiesString) //конструктор
        {
            this.subjectsCount = subjectsCount;
            this.teacherName = teacherName;
            subjects = new Subjects[this.subjectsCount];

            for (int dayOfTheMonth = 0; dayOfTheMonth < 30; dayOfTheMonth++)
            {
                this.opportunities[dayOfTheMonth / 7, dayOfTheMonth % 7] = opportunitiesString[dayOfTheMonth] == '+';
            }

            CountOpp();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    TOpp[i, j, 0] = opportunities[i, j];
                    TOpp[i, j, 1] = opportunities[i, j];
                }
            }
        }
        private void CountOpp() //подсчёт количества 
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (opportunities[i, j])
                    {
                        possibilitiesCount++;
                    }
                }
            }
        }
    }
}
