using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WhiskerGenome { FarLeft = 0, Left = 1, Middle = 2, Right = 3, FarRight = 4 }

public class Genome {
    public WhiskerGenome WhiskerGenome;

    //At what point the touching is too much
    public double TouchingSolidThreshold;
    public double TouchingEnemyThreshold;
    public double TouchingDangerousThreshold;

    //Which "reaction" pairs with which "instinct"
    public bool[] SolidReactionInstincts;
    public bool[] EnemyReactionInstincts;
    public bool[] DangerousReactionInstincts;

    //Which "reaction" pairs with "boredom" (lack of instinct)
    public bool[] DoNothingInstincts;

    //Whisker length
    public double WhiskerLength;

    public static Pair<Genome, Genome> Breed(Genome partnerOne, Genome partnerTwo) {
        var childOne = new Genome();
        var childTwo = new Genome();

        childOne.TouchingSolidThreshold = partnerOne.TouchingSolidThreshold;
        childOne.TouchingEnemyThreshold = partnerTwo.TouchingEnemyThreshold;
        childOne.TouchingDangerousThreshold = partnerOne.TouchingDangerousThreshold;
        childOne.SolidReactionInstincts = partnerOne.SolidReactionInstincts;
        childOne.EnemyReactionInstincts = partnerTwo.EnemyReactionInstincts;
        childOne.DangerousReactionInstincts = partnerOne.DangerousReactionInstincts;
        childOne.DoNothingInstincts = partnerTwo.DoNothingInstincts;
        childOne.WhiskerLength = partnerOne.WhiskerLength;

        childTwo.TouchingSolidThreshold = partnerTwo.TouchingSolidThreshold;
        childTwo.TouchingEnemyThreshold = partnerOne.TouchingEnemyThreshold;
        childTwo.TouchingDangerousThreshold = partnerTwo.TouchingDangerousThreshold;
        childTwo.SolidReactionInstincts = partnerTwo.SolidReactionInstincts;
        childTwo.EnemyReactionInstincts = partnerOne.EnemyReactionInstincts;
        childTwo.DangerousReactionInstincts = partnerTwo.DangerousReactionInstincts;
        childTwo.DoNothingInstincts = partnerOne.DoNothingInstincts;
        childTwo.WhiskerLength = partnerTwo.WhiskerLength;

        childOne.Mutate();
        childTwo.Mutate();

        return new Pair<Genome, Genome>(childOne, childTwo);
    }

    public void Mutate() {
        var instictChangeAmount = .02;
        var divisionAmount = 20;
        TouchingSolidThreshold += (0.5 - GenomeFactory.random.NextDouble()) / divisionAmount;
        TouchingEnemyThreshold += (0.5 - GenomeFactory.random.NextDouble()) / divisionAmount;
        TouchingDangerousThreshold += (0.5 - GenomeFactory.random.NextDouble()) / divisionAmount;

        for(int i = 0; i < SolidReactionInstincts.Length; i++) {
            if (GenomeFactory.random.NextDouble() < instictChangeAmount)
                SolidReactionInstincts[i] = !SolidReactionInstincts[i];
        }

        for (int i = 0; i < EnemyReactionInstincts.Length; i++) {
            if (GenomeFactory.random.NextDouble() < instictChangeAmount)
                EnemyReactionInstincts[i] = !EnemyReactionInstincts[i];
        }

        for (int i = 0; i < DangerousReactionInstincts.Length; i++) {
            if (GenomeFactory.random.NextDouble() < instictChangeAmount)
                DangerousReactionInstincts[i] = !DangerousReactionInstincts[i];
        }

        for (int i = 0; i < DoNothingInstincts.Length; i++) {
            if (GenomeFactory.random.NextDouble() < instictChangeAmount)
                DoNothingInstincts[i] = !DoNothingInstincts[i];
        }

        WhiskerLength += (0.5 - GenomeFactory.random.NextDouble()) * 2;

        WhiskerLength = WhiskerLength > 0 ? WhiskerLength : 0;
        TouchingSolidThreshold = TouchingSolidThreshold > 0 ? TouchingSolidThreshold : 0;
        TouchingEnemyThreshold = TouchingEnemyThreshold > 0 ? TouchingEnemyThreshold : 0;
        TouchingDangerousThreshold = TouchingDangerousThreshold > 0 ? TouchingDangerousThreshold : 0;
        TouchingSolidThreshold = TouchingSolidThreshold < 1 ? TouchingSolidThreshold : 1;
        TouchingEnemyThreshold = TouchingEnemyThreshold > 1 ? TouchingEnemyThreshold : 1;
        TouchingDangerousThreshold = TouchingDangerousThreshold > 1 ? TouchingDangerousThreshold : 1;
    }

    public override string ToString() {
        string str = "Solid : " + TouchingSolidThreshold + ", Enemy : " + TouchingEnemyThreshold + ", Dangerous : " + TouchingDangerousThreshold + "\n";
        foreach (var react in SolidReactionInstincts)
            str += react + " ";
        str += "\n";
        foreach (var react in EnemyReactionInstincts)
            str += react + " ";
        str += "\n";
        foreach (var react in DangerousReactionInstincts)
            str += react + " ";
        str += "\n";
        foreach (var react in DoNothingInstincts)
            str += react + " ";
        str += "\n";
        return str;
    }
}
