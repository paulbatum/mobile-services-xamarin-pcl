using System;
using Newtonsoft.Json;
using Shared;

namespace xamarinpcl
{

	public class ToDoItemWrapper : Java.Lang.Object
	{
		public ToDoItemWrapper (ToDoItem item)
		{
			ToDoItem = item;
		}

		public ToDoItem ToDoItem { get; private set; }
	}
}

