using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PoEValuablesHelper.Poe.Model;

namespace PoEValuablesHelper.Poe
{
    public class PoeServer
    {
        private readonly string _accName;
        private readonly string _sessionId;
        private readonly string _league;

        public PoeServer(string accName, string sessionId, string league)
        {
            _accName = accName;
            _sessionId = sessionId;
            _league = league;
        }

        public async Task<IList<Tab>> GetTabs()
        {
            string url =
            $"https://www.pathofexile.com/character-window/get-stash-items?accountName={_accName}&realm=pc&league={_league}&tabs=1&tabIndex=0";

            using var handler = GetHandler(url);
            using var client = new HttpClient(handler);
            var responseMessage = await client.GetAsync(url);
            var body = await responseMessage.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<TabsResponse>(body);

            return result?.Tabs;
        }

        public async Task<IDictionary<int, IList<Item>>> GetItemsFromTabs(IEnumerable<int> tabIndexes)
        {
            IDictionary<int, IList<Item>> result = new Dictionary<int, IList<Item>>();

            using var handler = GetHandler("https://www.pathofexile.com");
            using var client = new HttpClient(handler);

            foreach (var index in tabIndexes)
            {
                string url =
                    $"https://www.pathofexile.com/character-window/get-stash-items?accountName={_accName}&realm=pc&league={_league}&tabs=0&tabIndex={index}";

                var responseMessage = await client.GetAsync(url);
                var body = await responseMessage.Content.ReadAsStreamAsync();
                var response = await JsonSerializer.DeserializeAsync<TabsResponse>(body);
                result[index] = response?.Items ?? new List<Item>();
            }

            return result;
        }

        private HttpClientHandler GetHandler(string url)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Uri(url), new Cookie("POESESSID", _sessionId));
            return new HttpClientHandler { CookieContainer = cookieContainer };
        }
    }
}
