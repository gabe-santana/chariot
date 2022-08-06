using System.Text.RegularExpressions;

namespace CGS.Handler.Utils
{
    public static class Parser
    {
        private static Regex rg = new Regex(@"([^(,)]+)(?!.*\()");

        /// <summary>
        /// Get the params of conn e.g: conn(user_id, game_id) => [user_id, game_id]
        /// </summary>
        /// <returns></returns>
        public static string[] GetParams(string cmd)
        {
            return rg.Matches(cmd).Select(match => match.Value.Trim()).ToArray();
        }
    }
}
