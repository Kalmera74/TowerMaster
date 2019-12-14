using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingShoot : MonoBehaviour
{
    // protected
    protected Joystick joystick;

    // public
    public GameObject BasePointer;
    public GameObject Pointer;
    public float PointerDistance = 20;
    public float PoinerSpeed = 5;
    public int Slice = 6;

    // private
    bool isCreated = false;
    int Handler = 0;
    int TopDist = 0;
    Dictionary<int, GameObject> CreatedList = new Dictionary<int, GameObject>();

    void Start () {
        joystick = FindObjectOfType<Joystick>();
    }

    void Update () {
        // Pointer ın karakterden uzaklığı
        var dist = Vector3.Distance(BasePointer.transform.position, Pointer.transform.position);

        var x = -joystick.Vertical * PoinerSpeed;
        var z = joystick.Horizontal * PoinerSpeed;

        Pointer.transform.position = new Vector3(
                x, Pointer.transform.position.y, z);

        // if (dist < PointerDistance) {
        //     Pointer.transform.position = new Vector3(
        //         currentPointerPosition.x + x, currentPointerPosition.y, currentPointerPosition.z + z);
        // }

        var intDist = (int)dist;
        var oneSlice = dist / Slice;

        if (intDist == 0) {
            foreach(KeyValuePair<int, GameObject> obj in CreatedList) {
                Destroy(obj.Value);
            }
            CreatedList.Clear();
        }

        if (intDist < TopDist && CreatedList.Count > 0) {
            Destroy(CreatedList[TopDist]);
            CreatedList.Remove(TopDist);
            TopDist = intDist;
        }

        if (intDist > 0 && !CreatedList.ContainsKey(intDist) && CreatedList.Count < Slice - 1) {
            TopDist = intDist;
            Debug.Log(oneSlice * intDist);
            Debug.Log(intDist);
            Debug.Log(oneSlice);
            var vector = BasePointer.transform.position - new Vector3(oneSlice * dist, 0, 0);
            vector.y = Pointer.transform.position.y;
            vector.z = -(BasePointer.transform.position - Pointer.transform.position).z;
            var clone = Instantiate(Pointer, vector, Pointer.transform.rotation, transform);
            CreatedList.Add(intDist, clone);
        }
        else {
            foreach(KeyValuePair<int, GameObject> obj in CreatedList) {
                var vector = BasePointer.transform.position - new Vector3(oneSlice * obj.Key, 0, 0);
                vector.y = Pointer.transform.position.y;
                vector.z = -(BasePointer.transform.position - Pointer.transform.position).z;
                obj.Value.transform.position = vector;
            }
        }


    }
}
