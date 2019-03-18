using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataService.Infrastructure
{
    public class FirebaseSerivce<T>
    {
        private FirebaseClient Firebase { get; }

        public FirebaseSerivce()
        {
            var auth = "5ykcp6l5vgtzgkNKIGVrcn76KnOyX8WTnucvRJbR";
            var baseUrl = "https://shoesshop-822d0.firebaseio.com/";
            var option = new FirebaseOptions() { AuthTokenAsyncFactory = () => Task.FromResult(auth) };

            this.Firebase = new FirebaseClient(baseUrl, option);
        }


        public async Task GetDataAsync(string key)
        {
            var data = await this.Firebase.Child(key).OnceAsync<string>();

            foreach (var item in data)
            {
                Console.WriteLine(String.Format("the Async the Data from the Get Key ({0}) = {}. 1: {2}",
                    key,
                    item.Key,
                    item.Object));
            }

        }

        public async Task SetDataAsync(string key, T data)
        {
            var result = await this.Firebase.Child(key)
                .PostAsync(JsonConvert.SerializeObject(data));

            Console.WriteLine(string.Format("Post Async Data to key({0}) = {1}", result.Key, result.Object));
        }
    }
}
