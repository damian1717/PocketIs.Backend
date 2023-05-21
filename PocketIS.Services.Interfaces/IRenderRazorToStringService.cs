using System.Threading.Tasks;

namespace PocketIS.Services.Interfaces
{
    public interface IRenderRazorToStringService
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="viewName">view to render</param>
        /// <param name="model">data to render</param>
        /// <param name="htmlDecode">Defines whether or not to convert a string that has been HTML-encoded for HTTP transmission into a decoded string.</param>
        /// <returns>html as a string</returns>
        Task<string> RenderToStringAsync(string viewName, object model, bool htmlDecode);
    }
}
