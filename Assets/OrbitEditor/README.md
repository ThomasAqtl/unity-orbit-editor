# unity-orbit-system

## About

_unity-orbit-editor_ is a lightweight package you can use in any Unity project to quickly and easily setup and manage
**non-physics-based** orbit systems. It uses
[ellipsis parametric equation](https://en.wikipedia.org/wiki/Ellipse#Standard_parametric_representation)
to create a set of points that describes the ellipsis shape and make a body follow it.

## Getting started

1. Visit the release page to download the unity package containing the necessary code and import it into your Assets
   folder
2. Create an orbit system :
    - Via the editor menu : `GameObject`>`Orbit system`>`New orbit system`;
    - Manually : add an `OrbitSystem` component to any object.
3. Add new orbit or remove existing ones simply by adding/removing elements from the Orbit list in the inspector inside
   the `OrbitSystem` component.
4. Tweak orbit parameters to familiarize yourself with the tool!

## How it works

When creating an orbit system, a default initial orbit is instantiated as a children object with two components on it :

- `FollowPath` makes the object follow a given path (here a `Vector3` array) with a given period. The path that defines
  the orbit trajectory (that is, an ellipse) is
  generated inside the `Ellipsis` class (which is **not** a `MonoBehavior`)
- `LookAt` makes the object transform constantly look at a given target.

These components are self-contained and can be used easily across your project.

Each orbit is a simple GameObject with the components described above. Feel free to add any other components to make it
behave as you like! Both `FollowPath` and `LookAt` components only act on the `Transform` component.


