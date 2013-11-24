using UnityEngine;
using System.Collections;

// Sphere coordinator
public class SC : MonoBehaviour {

    public Vector3 position;
    public Vector3 direction;
    public float height;
    public float radius;

    void Awake() {
        position = new Vector3(0,0,1);
        direction = new Vector3(0,1,0);
    }

    public float GetAngleTo(Vector3 target) {
        float sign = Vector3.Dot(Vector3.Cross(direction, target - position), position) > 0 ? 1 : -1;
        return Vector3.Angle(direction, target - position) * sign;
    }

    public float GetAngleFromTo(Vector3 direction, Vector3 target) {
        float sign = Vector3.Dot(Vector3.Cross(direction, target - position), position) > 0 ? 1 : -1;
        return Vector3.Angle(direction, target - position) * sign;
    }

    public void RotateBy(float angle) {
        direction = Quaternion.AngleAxis(angle, position) * direction; 
    }

    public void SetPosition(Vector3 position) {
        this.position = position;
        Correct();
    }

    public void SetDirection(Vector3 direction) {
        this.direction = direction;
        Correct();
    }

    public void SetDirectionTo(Vector3 target) {
        direction = target -position;
        Correct();
    }

    public void Correct() {
        position = position.normalized;
        direction = direction - Vector3.Dot(direction, position) * position;
        direction = direction.normalized;
        if (direction.magnitude == 0) {
            direction = new Vector3(0,1,0);
        }
    }

    public void MoveForward(float speed) {
        position += direction * speed;

        Correct();
    }

    public void MoveSide(float speed) {
        position += GetSide() * speed;

        Correct();
    }

    public Vector3 GetForwardPosition(float dist) {
        Vector3 result = position + direction * dist;
        return result.normalized;
    }

    public Vector3 GetSide() {
        return Vector3.Cross(position, direction);
    }

    public bool IsColliding(GameObject go) {
        SC b = go.GetComponent<SC>();
        return b!=null&&Vector3.Distance(b.position,position)<radius+b.radius;
    }
    
    void Update () {
    
    }
}
