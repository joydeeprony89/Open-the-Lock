// See https://aka.ms/new-console-template for more information




// https://www.youtube.com/watch?v=Pzg3bCDY87w
string[] deadends = new string[] { "0201", "0101", "0102", "1212", "2002" };
string target = "0202";

Solution s = new Solution();
var answer = s.OpenLock(deadends, target);
Console.WriteLine(answer);


public class Solution
{
  public int OpenLock(string[] deadends, string target)
  {
    // there are two possibility wither increment or decrement
    // 0 if we increment it will become 1 and after decrement 0 by 1 it will be 9
    // 0000 -> 1000/9000
    //                0000 (root)
    // 1000/9000 0100/0900 0010/0090 0001/0009 - (childrens)
    // and this tree will keeo on growing 

    // why fill visited witj deadends ? coz deadends having those string that we dont want to visit 
    var visited = new HashSet<string>(deadends.ToList());
    // incase our target is already present in deadends then we can not proceed and can return -1
    if (visited.Contains("0000")) return -1;

    // perform BFS
    Queue<(string, int)> q = new Queue<(string, int)>();
    // it will always start with "0000"
    q.Enqueue(("0000", 0));
    while (q.Count > 0)
    {
      var (lck, turns) = q.Dequeue();
      if (lck == target) return turns;
      // we have to generate 8 childs for a lock
      var childs = Children(lck) ;
      foreach (var child in childs)
      {
        if (visited.Contains(child)) continue;
        visited.Add(child);
        q.Enqueue((child, turns + 1));
      }
    }

    List<string> Children(string lck)
    {
      var child = new List<string>();
      
      for (int i = 0; i < lck.Length; i++)
      {
        var charArr = lck.ToCharArray();
        char c = charArr[i];
        int val = c - '0';
        // increment
        var newVal = (val + 1) % 10;
        charArr[i] = (char)(newVal + '0');
        string incs = string.Join("", charArr);
        child.Add(incs);
        // decrement
        newVal = (val - 1 + 10) % 10;
        charArr[i] = (char)(newVal + '0');
        string decs = string.Join("", charArr);
        child.Add(decs);
      }

      return child;
    }
    return -1;
  }
}
