using CGS.Utils.Enums;


namespace CGS.SharedKernel.ResponseObjects
{
    public class MessageResponseObject
    {
        public MessageTypeEnum MessageType { get; set; }
        public string? Message { get; set; }
    }
}
