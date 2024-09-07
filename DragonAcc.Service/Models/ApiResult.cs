namespace DragonAcc.Service.Models
{
    public class ApiResult
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        public bool IsSuccess { get => String.IsNullOrEmpty(Message); }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResult"/> class.
        /// </summary>
        public ApiResult()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResult"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public ApiResult(object? data)
        {
            Data = data;
        }
    }
}
