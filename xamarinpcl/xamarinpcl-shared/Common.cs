﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Common
    {
        static Common()
        {
            InitializeClient();
        }

        public static MobileServiceClient MobileService;

        public static void InitializeClient(params DelegatingHandler[] handlers)
        {
            MobileService = new MobileServiceClient(
				@"https://xamarinpcl.azure-mobile.net/",
				null,
                handlers
            );
        }

        public static IMobileServiceTableQuery<ToDoItem> GetIncompleteItems()
        {
            var toDoTable = MobileService.GetTable<ToDoItem>();
            return toDoTable.Where(item => item.Complete == false);
        }
    }

    public class ToDoItem
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
    }
}
