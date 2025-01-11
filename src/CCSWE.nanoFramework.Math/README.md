[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework.Math/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework.Math/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Math.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Math/) 

# CCSWE.nanoFramework.Math

A fast, but not necessarily IEEE 754:2019 compliant, implementation of System.Math for .NET nanoFramework.

## Goals

1. Provide an "as fast as possible" implementation of System.Math at the expense of [IEEE 754:2019](https://ieeexplore.ieee.org/document/8766229) compliance
2. Look for opportunities to improve the performance of the IEEE 754:2019 complaint .NET nanoFramework [System.Math](https://github.com/nanoframework/System.Math) implementation.
 
At this time the first goal is primarily being achieved by removing specific behavior related to `double.NaN` and `float.Nan` values. The IEEE 754:2019 specification provides clear guidelines on how certain function should behave related to NaN values but this adds additional overhead that is not always required.

For the second goal I will port any improvements I find back into [System.Math](https://github.com/nanoframework/System.Math) implementation and eventually remove them from this library.