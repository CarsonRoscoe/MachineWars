using UnityEngine;
using System.Collections;
using System;

public class Whisker : MonoBehaviour {
    //How much the whisker is sensing the things it touches
	private double _touchingSolid { get; set; }
    private double _touchingEnemy { get; set; }
    private double _touchingDangerous { get; set; }

    //Genome used
    public Genome Genome { get; private set; }

    //Whether the whiskers are reacting to something or not
    private bool _isReactingToSolid { get { return _touchingSolid >= Genome.TouchingSolidThreshold; } }
    private bool _isReactingToEnemy { get { return _touchingEnemy >= Genome.TouchingEnemyThreshold; } }
    private bool _isReactingToDangerous { get { return _touchingDangerous >= Genome.TouchingDangerousThreshold; } }

    //Instincts
    private Action[] _instinctualActions;

    public double AngleFacing;
    public double Distance { get { return Genome.WhiskerLength; } }

    void Start () {
        _touchingDangerous = 0;
        _touchingEnemy = 0;
        _touchingSolid = 0;
    }

    // Update is called once per frame
    void Update () {
        float personAngle = gameObject.transform.rotation.eulerAngles.z;
        var newVec = (Quaternion.Euler(0, 0, (personAngle + (float)AngleFacing)) * Vector2.right);
        Vector3 newSpot = gameObject.transform.position + (newVec.normalized * (float)Distance);

        Debug.DrawLine(gameObject.transform.position, newSpot, Color.red);

        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(transform.position, (transform.position + newSpot).normalized, (float)Distance);

        _touchingDangerous = 0;
        _touchingSolid = 0;
        _touchingEnemy = 0;

        if (hitInfo.Length > 1) {
            for(int i = 2; i < hitInfo.Length; i++) {
                if (hitInfo[i].collider != null) {
                    var other = hitInfo[i].transform.GetComponent<IObjectType>();
                    if (other != null) {
                        var ratio = 1 - hitInfo[i].distance / Distance;
                        switch (other.Type) {
                            case ObjectType.Dangerous:
                                Sword sword = other as Sword;
                                if (sword != null && sword._amount > sword._maxAmount * .3) {
                                    _touchingDangerous = ratio;
                                }
                                break;
                            case ObjectType.Solid:
                                _touchingSolid = ratio;
                                break;
                            case ObjectType.Enemy:
                                _touchingEnemy = ratio;
                                break;
                        }
                    }
                }
            }
        }

        if (_isReactingToSolid) {
            for (int i = 0; i < Genome.SolidReactionInstincts.Length; i++) {
                if (Genome.SolidReactionInstincts[i]) {
                    _instinctualActions[i]();
                }
            }
        }

        if (_isReactingToEnemy) {
            for (int i = 0; i < Genome.EnemyReactionInstincts.Length; i++) {
                if (Genome.EnemyReactionInstincts[i]) {
                    _instinctualActions[i]();
                }
            }
        }

        if (_isReactingToDangerous) {
            for (int i = 0; i < Genome.DangerousReactionInstincts.Length; i++) {
                if (Genome.DangerousReactionInstincts[i]) {
                    _instinctualActions[i]();
                }
            }
        } 

        if (!_isReactingToSolid && !_isReactingToEnemy && !_isReactingToDangerous) {
            for (int i = 0; i < Genome.DoNothingInstincts.Length; i++) {
                if (Genome.DoNothingInstincts[i]) {
                    _instinctualActions[i]();
                }
            }
        }
    }

    public void Init(Action[] reactions, Genome genome) {
        _instinctualActions = reactions;
        Genome = genome;
    }
}
