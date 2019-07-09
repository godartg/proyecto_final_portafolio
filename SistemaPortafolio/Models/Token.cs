using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaPortafolio.Models
{
    public class Token
    {
        //public const string token = "EwCAA8l6BAAURSN/FHlDW5xN74t6GzbtsBBeBUYAAb4NstM1kwCNPuRe4O0zWvG3yq2wZtx8cWPHf6JnKhpiGdEPQLZgjNI7wp78HY1GHeqlNCEzGmV6GaWlocItXqVhFNvs5Jg7YtHZgVl2xLkfF1DgIeZfmNcA0nEHoLT/T+ta+KTeKtfo1/Wk/K3ssjHV+MgT0hIOsGtFY5PouQdhKWSCkmsm4SR4xlg7hw/PYKpKWEzI6ZrAfM/9GSTTr54u8rZMmc8ZyfwiAYyviERpApNovm1fq9rXoZ0bwcw7rmwRQ6pQRkuY7FhyjOKXwwf6w/1bZ7WPekMpR14Sznb+BPNNJvodEDyikrJGBaI8qFfXWGPzmfEqGp/Lo24OXAgDZgAACJO4UsMAd2qpUAKsp+rlguV1KqQKXwCk25xHx72g0gelFs/f4sUPJSbTvej4bG1qnWLf14yKSqn9xqZvy7wdqPc4LZor/MhCZU194GD3Yw5DeB2jzMKyvjSQA/Eq9SkarTdlLmgQ7mXYKeO10qQeQRWGxKi63PPxndd95m7fiXqo/KPsDQBcMeLBFwfgICK8cPsHgkSpZ78vSOo+gDH1i/m0yAEOfscN3ipCWbqgXYdKuf+mcaN/nCvM4LgSDpxR2CymWP7o6xAZRDb0SScsp5XpyY4CPs6ZpjDCUfRp4Xg/CL7Jf8w5lVKMMxfvAZylEOfOKdh5Brz/sEBqqvl4GkERUttC2PaySmwOaXI3fqBW44KJ1NLq5GFHouL6l9YXDJZvlIYNSa97vOHq1vDbQP+B7Ofzz29FQjElbHJPqJxUiPODqzljtiQ3WN+44c4UnhO0XWb1ttHauC/4KJVOoUvdykFdolV3kIKRTU609LIZbChyRXj5fshWHz+hYq0qty/ARtoHy+QWS0Bd2uc4KpXJnTZegdxQ9ebjIzccAPO8OfsFP41lbhjTqTvU/xN4AfYOOp7pf+zHwXAxq8HcgHlNjxpKwQHPKKbfHkAHCdjPMF+WQF05BPhfGq2cGqGuacP7+cs7EPU0mhu17RljEFnHCrMlOT8PnhOG90+9IPB9EUBw6h2GrsehCggPSjhYS5BvS0vE9KbQ8Z7CYKzXfmW7Bi92p9olqcMVmREbFHPDxbLETeIyCC9WHgVKZfa1930b0Bqk0bU/Un1hUiF7Iqv5LMOcGATx1xcOgwI=";
        /// <summary>
        /// clientId of you office 365 application, you can find it in https://apps.dev.microsoft.com/
        /// </summary>
        public const string ClientId = "27523aa9-8f8b-4982-960e-1d1d1a701a81";
        /// <summary>
        /// Password/Public Key of you office 365 application, you can find it in https://apps.dev.microsoft.com/
        /// </summary>
        public const string Secret = "sbpiXDG694*ypmDHIQ51:]=";
        /// <summary>
        /// Authentication callback url, you can set it in https://apps.dev.microsoft.com/
        /// </summary>
        public const string CallbackUri = "https://localhost:44382/Admin/Home/OnAuthComplate";
    }
}