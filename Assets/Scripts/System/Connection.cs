using UnityEngine;

public class Connection
{
    public static bool Check()
    {
        return (Application.internetReachability !=
            NetworkReachability.NotReachable);
    }
}