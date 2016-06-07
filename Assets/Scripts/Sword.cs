using UnityEngine;
using System.Collections;

public class Sword : IObjectType {
    private bool _attacking;
    private bool _done;
    public int _amount;
    public int _maxAmount;
    public bool IsAttacking { get { return _attacking; } }
    private int killingBonus = 0;

	void Start () {
        Type = ObjectType.Dangerous;
        _attacking = false;
        _done = false;
        _amount = 0;
        _maxAmount = 120;
    }
	
	void Update () {
	    if (_attacking) {
            if (_done) {
                gameObject.transform.Rotate(0, 0, 2);
                _amount -= 2;
                if (_amount <= 0) {
                    _done = false;
                    _attacking = false;
                    _amount = 0;
                }
            } else {
                gameObject.transform.Rotate(0, 0, -8);
                _amount += 8;
                if (_amount >= _maxAmount) {
                    _done = true;
                }
            }
        }
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (!_attacking)
            return;

        var otherObj = collision.transform.gameObject;
        var parentObj = gameObject.transform.parent.gameObject;
        var otherObjPerson = otherObj.GetComponent<Person>();

        if (otherObj != parentObj && otherObjPerson != null && _amount > 50) {
            otherObjPerson.Killed();
            parentObj.GetComponent<Person>().Score += 4 + killingBonus++;
        }
    }

    public void Attack() {
        if (_attacking)
            return;

        _attacking = true;
        _done = false;
    }
}
