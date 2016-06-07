using UnityEngine;
using System.Collections;
using System;

public class GenomeFactory : MonoBehaviour {
    public static GenomeFactory instance;
    public static System.Random random;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        random = new System.Random();
    }

    public Genome CreateFreshGenome(WhiskerGenome type) {
        var genome = new Genome();
        var reactionChance = 0.04;

        Func<bool> randomBool = () => {
            return random.NextDouble() < reactionChance;
        };

        genome.WhiskerGenome = type;

        /*genome.TouchingSolidThreshold = random.NextDouble() * .8 + .1;
        genome.TouchingEnemyThreshold = random.NextDouble() * .8 + .1;
        genome.TouchingDangerousThreshold = random.NextDouble() * .8 + .1;

        genome.SolidReactionInstincts = new bool[5] { randomBool(), randomBool(), randomBool(), randomBool(), randomBool() };
        genome.EnemyReactionInstincts = new bool[5] { randomBool(), randomBool(), randomBool(), randomBool(), randomBool() };
        genome.DangerousReactionInstincts = new bool[5] { randomBool(), randomBool(), randomBool(), randomBool(), randomBool() };
        genome.DoNothingInstincts = new bool[5] { randomBool(), randomBool(), randomBool(), randomBool(), randomBool() };

        genome.WhiskerLength = random.NextDouble() * 4 + 2;*/

        genome.TouchingSolidThreshold = 1;
        genome.TouchingEnemyThreshold = 1;
        genome.TouchingDangerousThreshold = 1;

        genome.SolidReactionInstincts = new bool[5] { false, false, false, false, false };
        genome.EnemyReactionInstincts = new bool[5] { false, false, false, false, false };
        genome.DangerousReactionInstincts = new bool[5] { false, false, false, false, false };
        genome.DoNothingInstincts = new bool[5] { false, false, false, false, false };

        genome.WhiskerLength = 2;

        //genome.Mutate();

        return genome;
    }
}
