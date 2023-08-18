namespace IndentityLogic.Core
{
    public class AccountResult<TValue, TError>
    {
        public TValue Value { get; private set; }
        public TError? Errors { get; private set; }
        public bool IsSuccessful { get; private set; }

        public AccountResult(TValue value = default, bool isSuccessful = true, TError? errors = default)
        {
            Value = value;
            IsSuccessful = isSuccessful;
            Errors = errors;
        }
    }
}
