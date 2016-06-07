# MachineWars
C# Unity Machine Learning (Neural Network) Project

Created over the span of 8 hours, I created this as a personal challenge to try and make a neural network from scratch with a custom genetic algorithm for breeding the neural networks. I created this by following my knowledge of the NEAT algorithm, but taking a slightly different approach to it by using whiskers to get inputs from the world, which also allowed me to have a range of how hard this input was being pressured (is the whisker slightly touching something? Is it pressing against it hard? etc.).

# "Game" Portion

30 players spawn. Every 6 seconds the game ends and ranks how the AI performed. The top 15 players breed with one-another, while the bottom 15 players are killed and replaced by the children of the top 15 players. Every one of the final 30 players is then slightly mutated before the next round.

# Neural-Network Setup

Every person in the game had 5 whiskers which they used to sense the world.

Each whisker had its own genome.

Each whisker would respond to touch, the closer to the person along the whisker the collision is, the stronger the sensation.

Each whisker responds to three types of collisions: players (enemies), dangerous things (enemy swords in mid swing) and neutrals (walls).

Each player/whisker had six instinctual responses: Rotate left, rotate right, move forward, move backwards, swing a sword or do nothing.

Each whisker had thresholds for everything, essentially giving each the power to think "how close does x have to be to the player before I react, and which of my instincts (if any) will I react with).

# Genetic-Algorithm Setup

After 6 seconds the game would score the players and breed the next generation. Their scoring followed the following logic:

* Killing gave points
* Killing spree's gave extra points to encourage more killing
* Moving gave points, however it was a flat amount. Moving a lot was not better than moving a bit, but both were better than not moving at all.
* Staying alive gave points for every two seconds it was alive to encourage not dying to dangerous things

The top 15 placing people were mutated slightly and then bred. Essentially child A got every even member of parent A's genome and every odd member of parent B's genome, while child B got every odd member of parent A's genome and every even member of parent B's genome. This made the two children exact opposites, while still being direct descendants of their parents. After this, the children would both mutate slightly before the next round.
