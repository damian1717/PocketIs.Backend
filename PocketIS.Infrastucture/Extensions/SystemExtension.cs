﻿using Newtonsoft.Json;

namespace PocketIS.Infrastucture.Extensions
{
    public static class SystemExtension
    {
        public static T DeepClone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
