using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Test : MonoBehaviour
{
	void Start ()
    {
        HotCold();
	}

    /// <summary>
    /// Coldの場合はストリーム(Observable)が動いてない
    /// Hotにする事でSubscribeする前に動く
    /// </summary>
    private void HotCold()
    {
        var subject = new Subject<string>();
        var sourceObservable = subject.AsObservable();
        var stringObservable = sourceObservable.Scan((p, c) => {
            //Debug.Log("p:" + p);
            //Debug.Log("c:" + c);
            return p + c;
        });//.Share(); // Publish(); // subjectのObservable(登録)を返す

        // stringObservable.Connect();

        subject.OnNext("A");
        subject.OnNext("B");

        stringObservable.Subscribe(msg => Debug.Log("msg:" + msg));

        subject.OnNext("C");
        subject.OnNext("D");

        subject.OnCompleted();
    }
}
