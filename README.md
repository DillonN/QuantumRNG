# QuantumRNG

This repo contains the source for the paper **[Improving Classical Cryptography with Quantum Random Number Generation](QRNG-Report.pdf)**. 

The project `RandomTester` in the `src` folder is used as a simple benchmark for two types of pseudo-RNGs exposed in .NET:

* Managed .NET PRNG implementation (`System.Random`)
* Windows OS-level cryptographically-secure PRNG (`System.Security.Cryptography.RandomNumberGeneratorImplementation`)

The project itself has unused code for analyzing the quality or "randomness" of the output. Unfortunately this testing was not pursued as the information was not available for other compared RNGs. It is left for a complete reference of the project code as it was at the time of research.
