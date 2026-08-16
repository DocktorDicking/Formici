# 🐜 Ant Colony — Development Roadmap

A 2D ant colony simulation/game built with **C#/.NET + MonoGame**.

The long-term goal is to simulate realistic ant colonies inside player-managed formicariums.

The player does **not** directly control ants or colonies. Instead, the player provides an environment, observes the colony, performs research, earns Ant Credits, and eventually manages multiple colonies and species.

The first species will be **Lasius niger (Black Garden Ant)**.

---

# 🎯 Core Vision

The game should feel like keeping and observing a real ant colony.

The player:

- Places a queen into a formicarium.
- Sets up and maintains the environment.
- Provides food and water.
- Observes colony behavior.
- Conducts research.
- Earns Ant Credits.
- Purchases better equipment.
- Eventually manages multiple colonies.

The player does **not** directly command:

- The queen.
- Individual workers.
- Foraging.
- Nest selection.
- Brood care.
- Colony organization.
- Nest relocation.

The colony should make those decisions autonomously.

---

# 🧠 Core Design Principles

- [ ] Ants are autonomous.
- [ ] The player is primarily an observer and caretaker.
- [ ] The queen decides where to establish the nest.
- [ ] The colony decides when and whether to relocate.
- [ ] Simple rules should create complex emergent behavior.
- [ ] Simulation logic must remain independent from MonoGame rendering.
- [ ] Simulation time must be independent from rendering FPS.
- [ ] Biological realism should be introduced gradually.
- [ ] Avoid hard-coded species-specific behavior where possible.
- [ ] Every feature should be broken into small milestones.
- [ ] A milestone should ideally take between 30 minutes and a few hours.
- [ ] Every milestone should ideally produce something observable.
- [ ] Avoid building systems that are not needed yet.
- [ ] Prioritize a fun simulation over graphical fidelity.

---

# 🏗️ Long-Term Architecture

Eventually the project should look roughly like:

    AntColony.sln
    │
    ├── AntColony.Game
    │   ├── MonoGame
    │   ├── Rendering
    │   ├── Input
    │   ├── UI
    │   └── Camera
    │
    ├── AntColony.Simulation
    │   ├── World
    │   ├── Formicarium
    │   ├── Colony
    │   ├── Queen
    │   ├── Worker
    │   ├── Brood
    │   ├── Nest
    │   ├── Food
    │   ├── Environment
    │   ├── Pheromones
    │   └── SimulationClock
    │
    └── AntColony.Tests
        └── Simulation tests

Dependency direction:

    AntColony.Game
          ↓
    AntColony.Simulation

The simulation must never depend on MonoGame.

For example, the simulation can know about:

- Position
- Velocity
- State
- Age
- Energy
- Health
- Needs

But should not know about:

- Texture2D
- SpriteBatch
- GameTime
- MonoGame rendering APIs

---

# 🟢 Phase 0 — Project Foundation

Goal: Get MonoGame running and establish the minimum foundation.

## 0.1 — Create the project

- [ ] Create MonoGame DesktopGL project
- [ ] Target modern .NET
- [ ] Create Git repository
- [ ] Create initial README
- [ ] Verify project builds
- [ ] Verify game launches

**Result:** Empty MonoGame window.

---

## 0.2 — Basic game loop

- [ ] Understand `Game.Initialize()`
- [ ] Understand `Game.LoadContent()`
- [ ] Understand `Game.Update()`
- [ ] Understand `Game.Draw()`
- [ ] Add basic delta-time handling
- [ ] Add basic debug logging

**Result:** A functioning game loop.

---

## 0.3 — Basic world

- [ ] Create world coordinate system
- [ ] Define world width
- [ ] Define world height
- [ ] Draw simple background
- [ ] Add world boundaries
- [ ] Add debug coordinates

**Result:** A simple 2D world exists.

---

## 0.4 — Camera

- [ ] Create camera
- [ ] Implement camera position
- [ ] Implement camera zoom
- [ ] Implement camera movement
- [ ] Separate screen coordinates from world coordinates

**Result:** The camera can move around the world.

---

## 0.5 — Basic entity

- [ ] Create basic entity representation
- [ ] Add position
- [ ] Add update method
- [ ] Add basic rendering representation
- [ ] Create a test entity

**Result:** A visible object exists in the simulation.

---

# 🟢 Phase 1 — The Formicarium

Goal: Establish the fundamental container for a colony.

## 1.1 — Formicarium

Create:

    Formicarium
    ├── Bounds
    ├── Environment
    └── Colony

- [ ] Create `Formicarium`
- [ ] Define formicarium bounds
- [ ] Create empty environment
- [ ] Create empty colony

**Result:** The game contains a defined habitat.

---

## 1.2 — Formicarium rendering

- [ ] Draw formicarium boundary
- [ ] Draw background
- [ ] Draw simple walls
- [ ] Create basic visual distinction between areas

**Result:** The game visually resembles a small formicarium.

---

## 1.3 — Nest

Create:

    Nest
    ├── Position
    ├── Bounds
    └── Condition

- [ ] Create `Nest`
- [ ] Define nest position
- [ ] Define nest bounds
- [ ] Render nest
- [ ] Detect whether an entity is inside the nest

**Result:** The simulation understands "inside nest" and "outside nest."

---

## 1.4 — Foraging area

- [ ] Define area outside the nest
- [ ] Detect whether an entity is inside/outside nest
- [ ] Add basic terrain representation

**Result:** The formicarium has a nest and a foraging area.

---

# 🟢 Phase 2 — The Queen

Goal: The player places the queen, but the queen decides where she lives.

## 2.1 — Queen entity

Create:

    Colony
    └── Queen

- [ ] Create `Queen`
- [ ] Add position
- [ ] Add age
- [ ] Add health
- [ ] Add energy
- [ ] Add queen state

**Result:** A queen exists independently from workers.

---

## 2.2 — Player places queen

- [ ] Allow player to click inside formicarium
- [ ] Spawn queen at clicked location
- [ ] Prevent multiple queens
- [ ] Prevent placing queen outside formicarium

**Result:** The player can start a colony.

---

## 2.3 — Queen movement

- [ ] Add movement speed
- [ ] Add basic wandering
- [ ] Keep queen inside formicarium
- [ ] Render queen movement

**Result:** The queen behaves like an autonomous creature.

---

## 2.4 — Nest sites

Create:

    NestSite
    ├── Position
    └── Suitability

- [ ] Create nest-site representation
- [ ] Allow world to contain possible nest locations
- [ ] Give sites a suitability score
- [ ] Visualize nest sites during debugging

**Result:** The queen can evaluate potential homes.

---

## 2.5 — Queen searching

Initial queen state machine:

    SearchingForNest
            ↓
    EvaluatingLocation
            ↓
       NestSelected

- [ ] Add queen states
- [ ] Make queen search for suitable locations
- [ ] Make queen evaluate nearby locations
- [ ] Add basic randomness
- [ ] Prevent queen from immediately settling

**Result:** The queen independently searches for a home.

---

## 2.6 — Queen chooses nest

- [ ] Select best suitable location
- [ ] Add controlled randomness to selection
- [ ] Move queen toward selected location
- [ ] Establish nest
- [ ] Change queen state to `Settled`

**Result:**

    Player places queen
            ↓
    Queen searches
            ↓
    Queen evaluates
            ↓
    Queen chooses
            ↓
    Queen establishes nest

This is the first complete autonomous colony-start sequence.

---

# 🟢 Phase 3 — Simulation Time

Goal: Establish a proper simulation clock.

## 3.1 — Simulation tick

- [ ] Create `SimulationClock`
- [ ] Create simulation tick
- [ ] Separate simulation time from rendering FPS
- [ ] Allow simulation systems to update using simulation time

**Result:** The simulation has its own concept of time.

---

## 3.2 — Hours and days

- [ ] Add hours
- [ ] Add days
- [ ] Convert ticks into simulation time
- [ ] Display current simulation time

Example:

    Day 1
    08:32

---

## 3.3 — Pause

- [ ] Add pause
- [ ] Stop simulation while paused
- [ ] Continue rendering while paused

---

## 3.4 — Simulation speed

Add:

    Pause
    1x
    2x
    5x
    10x

- [ ] Add 1x
- [ ] Add 2x
- [ ] Add 5x
- [ ] Add 10x
- [ ] Add UI indicator

**Result:** The player can observe the colony at different speeds.

---

# 🟢 Phase 4 — Queen Lifecycle

Goal: Make the queen the beginning of a real colony lifecycle.

## 4.1 — Queen age

- [ ] Increase queen age over simulation time
- [ ] Store age
- [ ] Display queen age

---

## 4.2 — Queen energy

- [ ] Add energy
- [ ] Add energy consumption
- [ ] Allow energy recovery

---

## 4.3 — Queen resting

- [ ] Add resting state
- [ ] Allow queen to remain stationary
- [ ] Add resting duration

---

## 4.4 — Queen roaming

Add:

    Settled
       ↓
    Roaming
       ↓
    Settled

- [ ] Occasionally move queen around nest
- [ ] Keep movement inside nest
- [ ] Return queen to preferred location
- [ ] Add randomness to roaming frequency

**Result:** Queen is not just a static egg generator.

---

## 4.5 — Queen senses brood

- [ ] Allow queen to detect nearby brood
- [ ] Add basic brood awareness
- [ ] Add preference for remaining near brood

---

## 4.6 — Egg production

- [ ] Add egg-production timer
- [ ] Create `Egg`
- [ ] Add egg to colony
- [ ] Track egg count
- [ ] Render eggs

**Result:** The colony starts growing.

---

# 🟢 Phase 5 — Brood Development

Goal: Create the basic ant lifecycle.

    Egg
     ↓
    Larva
     ↓
    Pupa
     ↓
    Worker

## 5.1 — Egg

- [ ] Create egg entity
- [ ] Add development timer
- [ ] Add egg age
- [ ] Render egg

---

## 5.2 — Egg → Larva

- [ ] Add development threshold
- [ ] Convert egg into larva
- [ ] Remove egg
- [ ] Add larva

---

## 5.3 — Larva

- [ ] Create larva entity
- [ ] Add development timer
- [ ] Add basic food requirement
- [ ] Render larva

---

## 5.4 — Larva → Pupa

- [ ] Add development threshold
- [ ] Convert larva into pupa
- [ ] Remove larva
- [ ] Add pupa

---

## 5.5 — Pupa

- [ ] Create pupa entity
- [ ] Add development timer
- [ ] Render pupa

---

## 5.6 — Pupa → Worker

- [ ] Create worker from pupa
- [ ] Add worker to colony
- [ ] Remove pupa
- [ ] Render worker

### 🎉 Major milestone

> The player places a queen and eventually watches the first worker emerge.

---

# 🟢 Phase 6 — Worker Behavior

Goal: Give workers basic autonomous behavior.

## 6.1 — Worker entity

- [ ] Create `Worker`
- [ ] Add position
- [ ] Add age
- [ ] Add health
- [ ] Add energy
- [ ] Add state

---

## 6.2 — Worker movement

- [ ] Add movement
- [ ] Add random exploration
- [ ] Keep workers inside formicarium
- [ ] Render movement

---

## 6.3 — Worker knows its colony

- [ ] Associate worker with colony
- [ ] Give worker reference to nest
- [ ] Identify home nest

---

## 6.4 — Worker returns home

Add:

    Exploring
        ↓
    ReturningHome
        ↓
       Nest

- [ ] Create return-home state
- [ ] Move worker toward nest
- [ ] Detect arrival
- [ ] Enter nest

---

## 6.5 — Worker state machine

Initial states:

    Idle
    Exploring
    ReturningHome

- [ ] Create state system
- [ ] Implement transitions
- [ ] Add debug state display

---

## 6.6 — Worker exploration

- [ ] Worker leaves nest
- [ ] Worker explores
- [ ] Worker eventually returns
- [ ] Tune exploration randomness

**Result:** The first workers behave like actual colony members.

---

# 🟢 Phase 7 — Food

Goal: Introduce the first meaningful colony resource.

## 7.1 — Food source

Create:

    FoodSource
    ├── Position
    └── Amount

- [ ] Create food source
- [ ] Render food
- [ ] Place food manually for testing
- [ ] Allow food amount to decrease

---

## 7.2 — Worker detects food

- [ ] Add detection radius
- [ ] Detect nearby food
- [ ] Store detected food as a target

---

## 7.3 — Worker approaches food

Add:

    Exploring
        ↓
    FoodFound
        ↓
    MovingToFood

- [ ] Create target movement
- [ ] Move toward food
- [ ] Detect arrival

---

## 7.4 — Collect food

- [ ] Add collecting state
- [ ] Worker collects food
- [ ] Reduce food source amount
- [ ] Add carried food to worker

---

## 7.5 — Carry food

- [ ] Add `CarriedFood`
- [ ] Limit worker carrying capacity
- [ ] Render carried food for debugging

---

## 7.6 — Return home with food

    CarryingFood
          ↓
    ReturningHome
          ↓
         Nest

- [ ] Return to nest
- [ ] Detect arrival
- [ ] Deposit food

---

## 7.7 — Colony food storage

Create:

    Colony
    └── FoodStorage

- [ ] Create food storage
- [ ] Store collected food
- [ ] Display food reserves

### 🎉 Major milestone

> The first worker finds food and brings it back to the colony.

---

# 🟢 Phase 8 — Colony Needs

Goal: Make colony behavior driven by needs.

## 8.1 — Queen consumes food

- [ ] Queen consumes food
- [ ] Reduce colony food storage
- [ ] Add hunger/food need

---

## 8.2 — Workers consume food

- [ ] Workers consume food
- [ ] Add worker hunger
- [ ] Allow workers to eat

---

## 8.3 — Brood consumes food

- [ ] Larvae require food
- [ ] Add brood food requirement
- [ ] Track brood feeding

---

## 8.4 — Worker feeding

- [ ] Workers detect hungry brood
- [ ] Workers bring food to brood
- [ ] Feeding reduces brood need

---

## 8.5 — Worker prioritization

Basic decision priority:

    Hungry?
       ↓
      Eat

    Brood hungry?
       ↓
    Feed brood

    Food low?
       ↓
     Forage

- [ ] Implement needs
- [ ] Implement priorities
- [ ] Allow priorities to change dynamically

---

## 8.6 — Food shortage

- [ ] Allow food reserves to reach zero
- [ ] Increase hunger
- [ ] Reduce colony health when starving

---

## 8.7 — Worker death

- [ ] Add worker death condition
- [ ] Remove dead workers
- [ ] Track worker deaths
- [ ] Add basic age-related death

**Result:** Colony survival becomes an emergent system.

---

# 🟢 Phase 9 — Pheromones

Goal: Introduce emergent collective behavior.

## 9.1 — Pheromone grid

- [ ] Create pheromone grid
- [ ] Divide formicarium into cells
- [ ] Store pheromone strength

---

## 9.2 — Deposit pheromone

- [ ] Returning workers deposit pheromone
- [ ] Add pheromone strength
- [ ] Limit maximum pheromone

---

## 9.3 — Pheromone decay

- [ ] Reduce pheromone over time
- [ ] Remove negligible values
- [ ] Tune decay speed

---

## 9.4 — Detect pheromone

- [ ] Workers sense nearby pheromone
- [ ] Detect direction
- [ ] Detect strength

---

## 9.5 — Follow pheromone

- [ ] Bias movement toward stronger pheromone
- [ ] Add randomness
- [ ] Avoid deterministic movement

---

## 9.6 — Food trail

- [ ] Food-carrying workers create trails
- [ ] Other workers detect trails
- [ ] Other workers follow trails
- [ ] Tune trail strength

### 🎉 Major milestone

> One worker discovers food and the colony gradually establishes an emergent foraging trail.

---

# 🟢 Phase 10 — Better Queen Behavior

Goal: Make the queen behave more like a real animal.

## 10.1 — Nest awareness

- [ ] Queen senses nest conditions
- [ ] Add basic nest awareness
- [ ] Queen evaluates current nest

---

## 10.2 — Queen roaming

- [ ] Queen occasionally moves through nest
- [ ] Queen investigates different nest areas
- [ ] Queen returns to preferred location

---

## 10.3 — Queen and brood

- [ ] Queen detects brood
- [ ] Queen prefers suitable brood areas
- [ ] Add simple brood-related behavior

---

## 10.4 — Nest conditions

Create:

    NestCondition
    ├── Temperature
    ├── Moisture
    ├── Safety
    └── Space

- [ ] Create `NestCondition`
- [ ] Add temperature
- [ ] Add moisture
- [ ] Add safety
- [ ] Add available space

Initially these values can be very simple.

---

## 10.5 — Nest suitability

Replace the initial random suitability system with:

    Suitability =
        Temperature
      + Moisture
      + Safety
      + Space

- [ ] Calculate nest suitability
- [ ] Allow queen to sense suitability
- [ ] Allow queen to compare locations

---

## 10.6 — Queen decision-making

Create a reusable decision process:

    Environment
        ↓
    Queen senses
        ↓
    Queen evaluates
        ↓
    Queen chooses action

- [ ] Separate sensing from decision making
- [ ] Separate decision making from movement
- [ ] Add controlled randomness
- [ ] Avoid perfectly optimal decisions

---

# 🟢 Phase 11 — Colony Relocation

Goal: Allow the colony to autonomously move to a new nest.

This is deliberately a later feature.

## 11.1 — Bad nest

- [ ] Allow nest conditions to deteriorate
- [ ] Create unsuitable conditions
- [ ] Colony evaluates nest quality

---

## 11.2 — Relocation consideration

Add colony states:

    Established
        ↓
    ConsideringRelocation
        ↓
    SearchingForNest
        ↓
    Relocating

- [ ] Add `ConsideringRelocation`
- [ ] Add relocation threshold
- [ ] Allow colony to consider alternatives

---

## 11.3 — Search for new nest

- [ ] Queen searches for alternative locations
- [ ] Evaluate candidate locations
- [ ] Select new nest

---

## 11.4 — Queen relocates

- [ ] Move queen to new nest
- [ ] Update colony's active nest

---

## 11.5 — Move brood

- [ ] Workers identify brood requiring relocation
- [ ] Workers transport eggs
- [ ] Workers transport larvae
- [ ] Workers transport pupae

---

## 11.6 — Move food

- [ ] Workers transport stored food
- [ ] Maintain food during relocation

---

## 11.7 — Abandon old nest

- [ ] Old nest becomes inactive
- [ ] Workers finish relocation
- [ ] Colony establishes new nest

### 🎉 Major milestone

> The colony recognizes that its nest is unsuitable and moves house autonomously.

---

# 🟢 Phase 12 — Observation System

Goal: Make observing the colony meaningful.

## 12.1 — Event system

Create colony events:

    QueenLaidEgg
    WorkerBorn
    WorkerDied
    FoodDiscovered
    FoodReturned
    NestEstablished
    NestRelocated

- [ ] Create event abstraction
- [ ] Create first colony events
- [ ] Publish events from simulation

---

## 12.2 — Event history

- [ ] Record significant events
- [ ] Store recent events
- [ ] Display recent events

---

## 12.3 — Colony statistics

Track:

- [ ] Colony age
- [ ] Queen age
- [ ] Worker count
- [ ] Egg count
- [ ] Larva count
- [ ] Pupa count
- [ ] Food reserves
- [ ] Eggs produced
- [ ] Workers born
- [ ] Workers died
- [ ] Food collected
- [ ] Food consumed

---

## 12.4 — Colony overview

Create a basic information panel.

- [ ] Colony name
- [ ] Species
- [ ] Queen age
- [ ] Colony age
- [ ] Population
- [ ] Brood
- [ ] Food
- [ ] Current colony state

**Result:** The player can understand what the colony has been doing.

---

# 🟢 Phase 13 — Research

Goal: Turn observation into game progression.

## 13.1 — Research concepts

Create:

    Research
    ├── Name
    ├── Description
    ├── Requirements
    ├── Progress
    └── Reward

- [ ] Create research definition
- [ ] Create research progress
- [ ] Create research completion

---

## 13.2 — First observation research

Example:

    Observe successful foraging

- [ ] Detect successful foraging
- [ ] Increase research progress
- [ ] Display progress

---

## 13.3 — Research completion

- [ ] Complete research
- [ ] Unlock research entry
- [ ] Record completion

---

## 13.4 — Research rewards

- [ ] Define first research reward
- [ ] Apply reward
- [ ] Display reward

---

## 13.5 — Research log

- [ ] Display discovered knowledge
- [ ] Store completed research
- [ ] Create research UI

---

# 🟢 Phase 14 — Ant Credits

Goal: Introduce the game's progression currency.

## 14.1 — Player profile

Create:

    Player
    ├── AntCredits
    ├── Research
    └── Formicariums

- [ ] Create player state
- [ ] Add Ant Credits
- [ ] Add research state
- [ ] Add formicarium collection

---

## 14.2 — Earn Ant Credits

- [ ] Award credits for research
- [ ] Award credits for significant observations
- [ ] Create credit transaction system

---

## 14.3 — Display Ant Credits

- [ ] Add credit counter
- [ ] Show earned credits
- [ ] Show spending

---

## 14.4 — First purchase

- [ ] Create one purchasable item
- [ ] Add item cost
- [ ] Deduct credits
- [ ] Add item to player

### 🎉 Major milestone

The first complete meta-loop exists:

    Observe
       ↓
    Research
       ↓
    Earn Ant Credits
       ↓
    Buy something
       ↓
    Improve colony environment

---

# 🟢 Phase 15 — Formicarium Management

Goal: Give the player control over the environment while keeping ants autonomous.

## 15.1 — Player food placement

- [ ] Allow player to place food
- [ ] Allow different food amounts
- [ ] Colony reacts autonomously

---

## 15.2 — Water

- [ ] Add water source
- [ ] Connect water to moisture
- [ ] Allow colony to react to moisture

---

## 15.3 — Temperature

- [ ] Add formicarium temperature
- [ ] Allow temperature changes
- [ ] Connect temperature to colony behavior

---

## 15.4 — Humidity

- [ ] Add humidity
- [ ] Connect humidity to nest conditions
- [ ] Allow queen/colony to respond

---

## 15.5 — Lighting

- [ ] Add light level
- [ ] Add day/night cycle
- [ ] Allow species to respond differently later

---

## 15.6 — Nest modules

- [ ] Create modular nest system
- [ ] Add additional chambers
- [ ] Add chamber connections
- [ ] Allow colony to expand into new chambers

---

## 15.7 — Foraging area

- [ ] Expand foraging area
- [ ] Add simple terrain options
- [ ] Add environmental variation

---

## 15.8 — Formicarium upgrades

- [ ] Create upgrade system
- [ ] Add upgrade costs
- [ ] Connect upgrades to Ant Credits
- [ ] Allow upgrades to affect the simulation

---

# 🟢 Phase 16 — Multiple Colonies

Goal: Allow the player to maintain multiple independent colonies.

## 16.1 — Colony collection

Create:

    Player
    └── Formicariums
        ├── Formicarium 1
        ├── Formicarium 2
        └── Formicarium 3

- [ ] Store multiple formicariums
- [ ] Give each formicarium its own colony

---

## 16.2 — Colony overview

- [ ] Display all colonies
- [ ] Show basic statistics
- [ ] Show species
- [ ] Show colony age
- [ ] Show population

---

## 16.3 — Independent simulation

- [ ] Each colony has independent state
- [ ] Each colony has independent environment
- [ ] Each colony continues simulating

---

## 16.4 — Multiple feeding

- [ ] Feed individual colonies
- [ ] Track individual food supplies
- [ ] Prevent food from being shared automatically

---

## 16.5 — Colony switching

- [ ] Switch between formicariums
- [ ] Observe individual colonies
- [ ] Manage individual environments

### 🎉 Major milestone

> The player can maintain multiple living colonies at the same time.

---

# 🟢 Phase 17 — Species System

Goal: Add additional species without rewriting the simulation.

## 17.1 — Species definition

Create:

    AntSpecies
    ├── Name
    ├── Worker characteristics
    ├── Queen characteristics
    ├── Development times
    ├── Food preferences
    ├── Temperature preferences
    └── Colony characteristics

- [ ] Create species abstraction
- [ ] Move species-specific values out of ant classes
- [ ] Create species configuration

---

## 17.2 — Lasius niger definition

- [ ] Create Lasius niger species definition
- [ ] Configure worker characteristics
- [ ] Configure queen characteristics
- [ ] Configure development times
- [ ] Configure environmental preferences
- [ ] Configure colony characteristics

---

## 17.3 — Second species

- [ ] Add a second species
- [ ] Verify existing simulation still works
- [ ] Verify species differences are visible

---

## 17.4 — Species-specific behavior

- [ ] Identify genuinely species-specific behavior
- [ ] Add behavior extensions where needed
- [ ] Avoid large species-specific conditionals

**Result:** New species can be introduced without rewriting the simulation.

---

# 🟢 Phase 18 — Advanced Biology

Long-term simulation depth.

These systems should only be added when the core simulation is stable.

## Seasonal behavior

- [ ] Add seasons
- [ ] Add seasonal temperature changes
- [ ] Add seasonal food availability
- [ ] Add seasonal activity changes

---

## Temperature-dependent activity

- [ ] Define species temperature preferences
- [ ] Change worker activity based on temperature
- [ ] Change brood development based on temperature
- [ ] Change queen activity based on temperature

---

## Humidity

- [ ] Add species humidity preferences
- [ ] Make brood sensitive to humidity
- [ ] Allow ants to move brood based on humidity

---

## Diapause

- [ ] Add seasonal inactivity
- [ ] Add diapause state
- [ ] Reduce colony activity
- [ ] Resume activity after diapause

---

## Nuptial flights

- [ ] Add reproductive males
- [ ] Add new queens
- [ ] Add nuptial flight behavior
- [ ] Allow new colonies to originate from queens

---

## Queen aging

- [ ] Add queen lifespan
- [ ] Add age-related changes
- [ ] Add queen death
- [ ] Define colony behavior after queen death

---

## Worker specialization

- [ ] Add worker age
- [ ] Add age-dependent behavior
- [ ] Add task preferences
- [ ] Allow workers to transition between tasks

---

## Brood relocation

- [ ] Workers can move brood
- [ ] Brood responds to nest conditions
- [ ] Colony can redistribute brood

---

## Nest expansion

- [ ] Workers expand available nest space
- [ ] Add excavation/construction behavior
- [ ] Allow colony to create new chambers

---

## Food specialization

- [ ] Add different food types
- [ ] Add species preferences
- [ ] Add nutritional values
- [ ] Allow colonies to prioritize different foods

---

## Aphids

- [ ] Add plants
- [ ] Add aphids
- [ ] Add honeydew
- [ ] Allow ants to tend aphids
- [ ] Allow ants to harvest honeydew

---

## Predators

- [ ] Add simple predators
- [ ] Add threat detection
- [ ] Allow colonies to respond to threats

---

## Colony competition

- [ ] Add multiple colonies to same environment where appropriate
- [ ] Detect nearby colonies
- [ ] Add territorial behavior
- [ ] Add competition for resources

---

# 🟢 Phase 19 — Advanced Formicariums

Goal: Turn formicarium design into a meaningful part of gameplay.

## Nest construction

- [ ] Modular nest system
- [ ] Multiple chamber types
- [ ] Chamber connections
- [ ] Different nest materials
- [ ] Nest expansion

---

## Environmental control

- [ ] Heating
- [ ] Cooling
- [ ] Humidification
- [ ] Lighting
- [ ] Watering
- [ ] Environmental sensors

---

## Foraging environments

- [ ] Different substrate types
- [ ] Plants
- [ ] Stones
- [ ] Leaves
- [ ] Branches
- [ ] Natural hiding places

---

## Specialized equipment

- [ ] Automatic feeders
- [ ] Water systems
- [ ] Heating systems
- [ ] Cooling systems
- [ ] Humidity controls
- [ ] Observation cameras
- [ ] Environmental sensors

---

# 🟢 Phase 20 — Long-Term Progression

Goal: Create a deep long-term game loop.

## Research tree

- [ ] Create research categories
- [ ] Create research dependencies
- [ ] Create research tiers
- [ ] Unlock advanced equipment
- [ ] Unlock advanced species

---

## Species encyclopedia

- [ ] Record discovered species
- [ ] Record biological information
- [ ] Record observed behaviors
- [ ] Track species statistics

---

## Colony achievements

- [ ] First worker
- [ ] First successful foraging trail
- [ ] First colony relocation
- [ ] First large colony
- [ ] First successful overwintering
- [ ] First second colony
- [ ] Species-specific achievements

---

## Colony history

- [ ] Store colony history
- [ ] Track population over time
- [ ] Track food consumption
- [ ] Track worker births/deaths
- [ ] Track major events
- [ ] Track nest changes

---

## Colony lineage

Potential future system:

- [ ] Track queens
- [ ] Track offspring queens
- [ ] Track colony lineage
- [ ] Track generations
- [ ] Track inherited characteristics if eventually desired

---

# 🧪 Phase 21 — Simulation Quality

Long-term technical and biological refinement.

## Testing

- [ ] Add simulation unit tests
- [ ] Test brood development
- [ ] Test food consumption
- [ ] Test worker behavior
- [ ] Test queen behavior
- [ ] Test nest selection
- [ ] Test relocation

---

## Deterministic simulation

- [ ] Introduce seeded randomness
- [ ] Allow simulations to be reproduced
- [ ] Use deterministic tests for important behavior

---

## Performance

- [ ] Profile simulation
- [ ] Profile rendering
- [ ] Optimize large ant populations
- [ ] Optimize pheromone calculations
- [ ] Optimize multiple colonies
- [ ] Support thousands of ants where practical

---

## Save/load

- [ ] Create save format
- [ ] Save player state
- [ ] Save colonies
- [ ] Save ants
- [ ] Save environment
- [ ] Save research
- [ ] Save Ant Credits
- [ ] Load saved game
- [ ] Handle save versioning

---

# 🎮 Major Prototype Milestones

These are the major moments worth aiming for.

---

## 🐜 Prototype 0.0.1 — The Queen

    Launch
      ↓
    Empty formicarium
      ↓
    Place queen
      ↓
    Queen explores
      ↓
    Queen evaluates locations
      ↓
    Queen chooses nest
      ↓
    Queen settles

### Requirements

- [ ] Formicarium
- [ ] Queen
- [ ] Queen movement
- [ ] Nest sites
- [ ] Queen decision making
- [ ] Nest establishment

---

## 🥚 Prototype 0.0.2 — Colony Begins

    Queen
      ↓
    Egg
      ↓
    Larva
      ↓
    Pupa
      ↓
    First worker

### Requirements

- [ ] Simulation clock
- [ ] Egg
- [ ] Larva
- [ ] Pupa
- [ ] Worker
- [ ] Development timers

---

## 🍎 Prototype 0.0.3 — First Foraging

    Worker
      ↓
    Explore
      ↓
    Find food
      ↓
    Collect food
      ↓
    Return home
      ↓
    Store food

### Requirements

- [ ] Food source
- [ ] Food detection
- [ ] Food collection
- [ ] Food carrying
- [ ] Return-home behavior
- [ ] Colony food storage

---

## 〰️ Prototype 0.0.4 — Emergent Colony

    Food
      ↓
    Worker discovers food
      ↓
    Pheromone deposited
      ↓
    Other workers detect trail
      ↓
    More workers forage
      ↓
    Food trail strengthens

### Requirements

- [ ] Pheromone grid
- [ ] Pheromone deposition
- [ ] Pheromone decay
- [ ] Pheromone detection
- [ ] Pheromone following
- [ ] Emergent foraging

---

## 🏠 Prototype 0.0.5 — Autonomous Colony

    Nest deteriorates
      ↓
    Colony detects problem
      ↓
    Queen searches
      ↓
    New nest selected
      ↓
    Queen moves
      ↓
    Workers move brood
      ↓
    Colony establishes new nest

### Requirements

- [ ] Nest conditions
- [ ] Colony decision making
- [ ] Relocation state
- [ ] Queen relocation
- [ ] Brood transportation
- [ ] Food transportation
- [ ] New nest establishment

---

## 🔬 Prototype 0.0.6 — The Researcher

    Observe colony
      ↓
    Discover behavior
      ↓
    Complete research
      ↓
    Earn Ant Credits
      ↓
    Improve formicarium

### Requirements

- [ ] Observation events
- [ ] Statistics
- [ ] Research
- [ ] Research rewards
- [ ] Ant Credits
- [ ] First purchasable item

---

## 🏠 Prototype 0.0.7 — Formicarium Keeper

    Formicarium #1
          +
    Formicarium #2
          +
    Formicarium #3
          ↓
    Manage multiple autonomous colonies

### Requirements

- [ ] Multiple formicariums
- [ ] Multiple colonies
- [ ] Colony overview
- [ ] Colony switching
- [ ] Independent simulation
- [ ] Individual feeding

---

# 🧭 Recommended Immediate Roadmap

Do not think about the entire roadmap while implementing.

The immediate target is:

## Milestone 1

- [ ] Create MonoGame DesktopGL project
- [ ] Create Git repository
- [ ] Get empty window running

## Milestone 2

- [ ] Draw a simple formicarium
- [ ] Add world coordinates
- [ ] Add basic camera

## Milestone 3

- [ ] Create `Simulation`
- [ ] Create `Formicarium`
- [ ] Create `Colony`

## Milestone 4

- [ ] Create `Queen`
- [ ] Render queen as a simple sprite/shape
- [ ] Place queen with mouse

## Milestone 5

- [ ] Make queen wander
- [ ] Keep queen inside formicarium

## Milestone 6

- [ ] Create `NestSite`
- [ ] Add suitability
- [ ] Create candidate locations

## Milestone 7

- [ ] Queen evaluates candidates
- [ ] Queen chooses a location

## Milestone 8

- [ ] Queen walks to chosen location
- [ ] Nest is established

### 🎉 First playable prototype

At this point:

> **You place a queen into an empty formicarium and watch her autonomously choose where to establish her nest.**

Everything after this builds on that foundation.

---

# 🧠 Simulation Philosophy

Avoid creating one giant AI system that tells every ant what to do.

Prefer:

    Senses
      ↓
    Needs
      ↓
    Possible actions
      ↓
    Decision
      ↓
    Movement / Action
      ↓
    World changes
      ↓
    New senses

For example:

    Worker is hungry
          ↓
    Worker senses food
          ↓
    Food is nearby
          ↓
    Worker chooses foraging
          ↓
    Worker moves toward food
          ↓
    Worker collects food
          ↓
    Worker returns home
          ↓
    Colony food increases
          ↓
    Worker hunger decreases

The individual rules should remain relatively simple.

The complexity should emerge from many ants interacting with:

- Each other
- The colony
- The nest
- Food
- Pheromones
- The environment
- Their own needs

---

# 🐜 Ultimate Design Goal

The game should eventually reach a point where the player can say:

> **"I didn't tell them to do that."**

The player provides the habitat.

The player provides resources.

The player observes.

The ants make decisions.

The colony responds.

The simulation creates the story.

---

# 🚧 Features Intentionally Not Planned For Early Development

These should not distract from the first prototype:

- [ ] Ant Credits
- [ ] Research tree
- [ ] Multiple colonies
- [ ] Multiple species
- [ ] Advanced formicariums
- [ ] Shop
- [ ] Advanced UI
- [ ] Advanced graphics
- [ ] Complex nest construction
- [ ] Aphid farming
- [ ] Predators
- [ ] Colony competition
- [ ] Nuptial flights
- [ ] Breeding
- [ ] Advanced genetics

These are part of the long-term vision, not the initial implementation.

---

# ⭐ Development Rule

When adding a new feature, ask:

1. **Can this be split into smaller milestones?**
2. **Can I finish one part in an evening?**
3. **Will I be able to see the result?**
4. **Does it improve the simulation?**
5. **Can the feature be implemented without prematurely building future systems?**

If the answer to #1 is yes:

> **Split it.**

The goal is steady, visible progress rather than large unfinished systems.
