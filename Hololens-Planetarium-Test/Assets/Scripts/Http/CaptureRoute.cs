using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreamSocketHttpServer;
using System.Text;

[UnityHttpServer]
public class CaptureRoute : MonoBehaviour {

    public TextMesh text;

    [UnityHttpRoute("/capture")]
    public void RouteIndex(HttpRequest request, HttpResponse response) {
        string s = "";
        foreach(KeyValuePair<string, string> pair in request.Args)
        {
            s += pair.Key + " - " + pair.Value + "\n";
        }

        response.BodyData = Encoding.UTF8.GetBytes(s);
        text.text = s;
        
    }

   
}
