namespace MongoDbTest.Entities
{
    internal class BaseQuestion
    {
        public Guid Id { get; set; }
        public string? Question { get; set; }
        public bool IsCorrect { get; set; }
        public string? Answer { get; set; }
    }

    internal class InputQuestion: BaseQuestion
    {
        public int VerbId { get; set; }
        public int FormIndex { get; set; }
    }

    internal class TapQuestion: BaseQuestion
    {
        public List<int>? VerbIds { get; set; }
    }

    internal class SelectQuestion : BaseQuestion
    {
        public int VerbId { get; set; }
        public int FormIndex { get; set; }
        public List<string>? WrongVerbs { get; set; }
    }

}
