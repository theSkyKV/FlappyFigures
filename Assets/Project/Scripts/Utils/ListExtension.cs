using System.Collections.Generic;

namespace Project.Utils
{
	public static class ListExtension
	{
		public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> list)
		{
			var linkedList = new LinkedList<T>();
			foreach (var element in list)
			{
				linkedList.AddLast(new LinkedListNode<T>(element));
			}

			return linkedList;
		}
	}
}