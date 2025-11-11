# City Builder Game with Octree Optimization

A sophisticated 3D city building simulation game built in Unity, featuring advanced octree-based spatial optimization for efficient rendering and collision detection, along with a comprehensive traffic system.

![Unity Version](https://img.shields.io/badge/Unity-2019.4.23f1-blue.svg)
![License](https://img.shields.io/badge/License-MIT-green.svg)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)

## 🎮 Features

### 🏗️ **City Building System**
- **Grid-based Building Placement**: Precise building positioning with snap-to-grid functionality
- **Diverse Building Library**: Multiple building types including residential, commercial, and municipal structures
  - Basic Roads & Town Roads
  - Residential buildings (Small Houses, Brick Houses, Luxury Apartments)
  - Commercial structures (Federal Building, Fire House, Hospital)
  - Specialized buildings (Urban Gardens, Townhouses)
- **Real-time Construction**: Dynamic building placement and removal system
- **Save/Load System**: Persistent city data management

### 🚗 **Advanced Traffic System**
- **Intelligent Vehicle AI**: Vehicles with multiple behavioral states (Idle, Looking, Moving, Stopping, Waiting)
- **Dynamic Pathfinding**: Smart navigation through city road networks
- **Realistic Physics**: Vehicle acceleration, braking, and rotation mechanics
- **Traffic Management**: Automatic vehicle spawning and route optimization
- **Collision Avoidance**: Advanced algorithms to prevent vehicle collisions

### 🌳 **Octree Spatial Optimization**
- **Efficient Culling**: Octree-based frustum culling for improved rendering performance
- **Dynamic Spatial Partitioning**: Automatic subdivision based on object density
- **Scalable Architecture**: Handles large-scale cities with thousands of objects
- **Memory Optimization**: Reduced memory footprint through intelligent object management

### 🎮 **Player Systems**
- **First-Person Controller**: Immersive player character with camera controls
- **Interactive Hand System**: Direct building placement and manipulation
- **Time Management**: In-game clock system with day/night cycles
- **User Interface**: Intuitive canvas-based UI for city management

## 🏗️ Project Structure

```
Assets/
├── Scripts/
│   ├── Octree/                    # Octree spatial optimization system
│   │   ├── Octree.cs             # Main octree implementation
│   │   ├── OctreeNode.cs         # Individual octree nodes
│   │   └── CreateOctree.cs       # Octree creation utilities
│   ├── Systems/                   # Core game systems
│   │   ├── GameControlSystem.cs   # Main game controller
│   │   ├── VehiclesManager.cs     # Traffic system management
│   │   ├── RoadsManager.cs        # Road network management
│   │   ├── InputSystem.cs         # Player input handling
│   │   └── SaveLoadSystem.cs      # Data persistence
│   ├── GameLogic/                 # Game mechanics
│   │   ├── Vehicle.cs             # Vehicle AI and physics
│   │   ├── PlayerCharacter.cs     # Player controller
│   │   ├── CameraRig.cs          # Camera system
│   │   └── PlayerHand.cs         # Building placement system
│   ├── GameStructures/            # Building and structure definitions
│   └── ConstructionEditor/        # Building placement tools
├── BuildingLibrary/               # Prefab library for all buildings
├── Scenes/                        # Game scenes
├── Materials/                     # Rendering materials
├── Meshes/                        # 3D models and meshes
└── Prefabs/                       # Reusable game objects
```

## 🚀 Getting Started

### Prerequisites
- **Unity 2019.4.23f1 LTS** (recommended)
- **Windows OS** (primary development platform)
- **Git** for version control

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Ayen241/City-Builder-Game-with-Octree-Optimization.git
   ```

2. Open Unity Hub and add the project:
   - Click "Add" in Unity Hub
   - Navigate to the cloned project folder
   - Select the project folder

3. Open the project in Unity 2019.4.23f1 LTS

4. Load the main scene:
   - Navigate to `Assets/Scenes/`
   - Open `Scene.unity`

5. Press Play to start the game

### Controls
- **WASD**: Move player character
- **Mouse**: Look around
- **Left Click**: Place selected building
- **Right Click**: Remove building
- **Tab**: Open/Close building selection menu
- **Esc**: Pause menu
- **Key 1**: Top view camera
- **Key 2**: First-person view camera
- **Space**: Jump
- **Q/E**: Rotate camera

## 🔧 Technical Implementation

### Octree System
The octree implementation provides efficient spatial partitioning for:
- **Frustum Culling**: Only render objects visible to the camera
- **Collision Detection**: Fast proximity queries for vehicles and buildings
- **Memory Management**: Dynamic loading/unloading of distant objects

```csharp
// Example octree usage
Octree cityOctree = new Octree(cityObjects, minNodeSize: 10f);
List<GameObject> visibleObjects = cityOctree.GetVisibleObjects(camera);
```

### Vehicle AI States
```csharp
public enum AIState
{
    Idle,        // Vehicle is stationary
    Looking,     // Searching for destination
    StartMoving, // Beginning movement
    Moving,      // Active navigation
    Stoping,     // Deceleration phase
    Waiting      // Temporary pause
}
```

### Building System
The modular building system supports:
- **ScriptableObject-based** building definitions
- **Grid-aligned** placement system
- **Resource management** for construction costs
- **Zoning restrictions** for realistic city planning

## 🎯 Performance Optimizations

- **Octree Culling**: Reduces rendered objects by up to 80% in large cities
- **Object Pooling**: Efficient vehicle spawning and despawning
- **LOD System**: Multiple detail levels for distant buildings
- **Batch Rendering**: Optimized draw calls for similar objects
- **Async Loading**: Non-blocking scene transitions

## 📸 Screenshots & Demos

### Building Construction
![Building Construction](https://github.com/imaxs/CityBuilder-and-Traffic-System/blob/master/Images/5.gif)

### Road Laying System
![Road System](https://github.com/imaxs/CityBuilder-and-Traffic-System/blob/master/Images/4.gif)

### Traffic System
#### Uncontrolled Intersections
![Uncontrolled Intersection](https://github.com/imaxs/CityBuilder-and-Traffic-System/blob/master/Images/Img01.gif)

#### Traffic Light Management
![Traffic Lights 1](https://github.com/imaxs/CityBuilder-and-Traffic-System/blob/master/Images/img02.gif)
![Traffic Lights 2](https://github.com/imaxs/CityBuilder-and-Traffic-System/blob/master/Images/Img03.gif)

## 🔮 Future Enhancements

- [ ] **Advanced Traffic Lights**: Intersection management system
- [ ] **Economic System**: Currency and resource management
- [ ] **Population Simulation**: Citizens with needs and behaviors
- [ ] **Weather System**: Dynamic weather affecting traffic and construction
- [ ] **Multiplayer Support**: Collaborative city building
- [ ] **Mod Support**: Custom building and vehicle creation tools

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines
- Follow Unity C# coding conventions
- Comment complex algorithms thoroughly
- Test performance impact of changes
- Update documentation for new features

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## 🙏 Acknowledgments

- Unity Technologies for the game engine
- Community contributors for building assets
- Octree algorithm based on spatial partitioning research
- Traffic simulation inspired by real-world urban planning
- Original CityBuilder-and-Traffic-System by imaxs

## 📞 Support

For questions, bug reports, or feature requests:
- Create an issue on GitHub
- Contact: muhammadfazreen241@gmail.com

**Built with ❤️ using Unity 2019.4.23f1**
