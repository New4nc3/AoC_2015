class Reindeer
{
    public string Name { get; }
    public int Speed { get; }
    public int RunDuration { get; }
    public int RestTime { get; }

    public int FullCycleDuration { get; }

    public int Timer { get; private set; }
    public int TotalDistance { get; private set; }
    public int ScorePart2 { get; private set; }

    public Reindeer(string name, int speed, int duration, int restTime)
    {
        Name = name;
        Speed = speed;
        RunDuration = duration;
        RestTime = restTime;

        FullCycleDuration = duration + restTime;
        Timer = 0;
        TotalDistance = 0;
        ScorePart2 = 0;
    }

    public void ProcessSecond() =>
        TotalDistance += Timer++ % FullCycleDuration < RunDuration ? Speed : 0;

    public void IncreaseForLeading() =>
        ++ScorePart2;
}
