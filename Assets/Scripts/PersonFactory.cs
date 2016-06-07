using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class PersonFactory : MonoBehaviour {
    public int AmountOfWhiskers = 5;
    public int VisionRangeDegrees = 90;

    public static PersonFactory instance;
    public Transform PlayerType;

    private double scoreTotal = 0;
    private double deathsTotal = 0;
    private Genome[] lastGenome = null;
    private bool start = false;
    private List<Pair<int, Genome[]>> genomes = new List<Pair<int, Genome[]>>();
    private int generationCount = 1;
    private int populationCount = 0;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public GameObject CreateFreshPerson(float x, float y) {
        if (PlayerType == null)
            return null;

        var person = (Transform)Instantiate(PlayerType, new Vector3(x, y, 0), Quaternion.identity);

        for (int i = 0; i < AmountOfWhiskers; i++) {
            var whisker = person.gameObject.AddComponent<Whisker>();

            var genome = GenomeFactory.instance.CreateFreshGenome((WhiskerGenome)i);

            whisker.Init(person.gameObject.GetComponent<Person>().PossibleReactions(), genome);
            whisker.AngleFacing = -(VisionRangeDegrees / 2) + i * (VisionRangeDegrees / (AmountOfWhiskers-1));
        }

        return person.gameObject;
    }

    public GameObject CreatePerson(float x, float y, Genome[] genome) {
        if (PlayerType == null)
            return null;

        var person = (Transform)Instantiate(PlayerType, new Vector3(x, y, 0), Quaternion.identity);

        for (int i = 0; i < AmountOfWhiskers; i++) {
            var whisker = person.gameObject.AddComponent<Whisker>();

            whisker.Init(person.gameObject.GetComponent<Person>().PossibleReactions(), genome[i]);
            whisker.AngleFacing = -(VisionRangeDegrees / 2) + i * (VisionRangeDegrees / (AmountOfWhiskers - 1));
        }

        return person.gameObject;
    }

    public void RecyclePerson(Genome[] genome, int score) {
        deathsTotal++;
        scoreTotal += score;
        genomes.Add(new Pair<int, Genome[]>(score, genome));
        if (populationCount == deathsTotal) {
            CreateGeneration();
        }
    }

    public void CreateGeneration() {
        int xAmount = 10;
        int yAmount = 12;
        int countRate = 4;

        if (!start) {
            start = true;
            for (float i = -xAmount; i < xAmount; i += countRate) {
                for (float j = -yAmount; j < yAmount; j += countRate) {
                    CreateFreshPerson(i, j);
                    populationCount++;
                }
            }
        } else {
            Debug.Log("Generation " + generationCount++ + " Data\nAverage : " + (deathsTotal > 0 ? scoreTotal / deathsTotal : 0) + ", TotalScore : " + scoreTotal);
            deathsTotal = 0;
            scoreTotal = 0;
            int count = 0;
            List<Genome[]> newGen = new List<Genome[]>();
            Pair<int, Genome[]>[] oldGen = genomes.OrderByDescending(x => x.first).ToArray();
            for(int i = 0; i < oldGen.Length/2; i++) {
                newGen.Add(oldGen[i].second);
            }

            var amount = newGen.Count;
            for (int i = 0; i < amount; i += 2) {
                var childOne = new Genome[AmountOfWhiskers];
                var childTwo = new Genome[AmountOfWhiskers];

                for(int j = 0; j < AmountOfWhiskers; j++) {
                    newGen.ElementAt(i)[j].Mutate();
                    newGen.ElementAt(i+1)[j].Mutate();
                    var children = Genome.Breed(newGen.ElementAt(i)[j], newGen.ElementAt(i + 1)[j]);
                    childOne[j] = children.first;
                    childTwo[j] = children.second;
                    newGen.ElementAt(i)[j].Mutate();
                    newGen.ElementAt(i + 1)[j].Mutate();
                }

                newGen.Add(childOne);
                newGen.Add(childTwo);
            }


            for (float i = -xAmount; i < xAmount; i += countRate) {
                for (float j = -yAmount; j < yAmount; j += countRate) {
                    CreatePerson(i, j, newGen.ElementAt(count++));
                }
            }

            genomes = new List<Pair<int, Genome[]>>();
        }
    }
}
