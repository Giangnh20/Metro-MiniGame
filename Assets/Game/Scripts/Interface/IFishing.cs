public interface IFishing
{
    bool IsDoingFish { get; set; }
    bool IsFishing { get; set; }
    bool Fished { get; set; }

    void DoFishing(bool doing);
}