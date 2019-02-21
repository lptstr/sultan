# sultan [![Build status](https://ci.appveyor.com/api/projects/status/xhyyn53we4o20t3c?svg=true)](https://ci.appveyor.com/project/Kiedtl/sultan) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/41f5a32b2d63424c9681c62ab6cdb752)](https://www.codacy.com/app/lptstr/sultan?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=lptstr/sultan&amp;utm_campaign=Badge_Grade) ![GitHub](https://img.shields.io/github/license/lptstr/sultan.svg)

A tool to stress-test your server.
Sultan makes it easy to 'attack' your server to see how resiliant it would be should a malicious hacker try to DDOS your server.

Like this project? ![Say thanks!](https://img.shields.io/badge/Say%20Thanks-!-1EAEDB.svg)

## Usage
Sultan provides three attack methods as of v0.3.0: XerXes, SlowLoris, and DeathPing.

Basic syntax:
```
Usage: .\sultan slowloris [host] [port] [socket_count] [useSSL? (true|false)]
Usage: .\sultan xerxes [host] [port] [connections] [threads]
Usage: .\sultan deathping [host]
```

### XerXes
Ths attack mimicks the so-called XerXes DOS code online - by creating hundreds or thousands of sockets on the target server and repeatedly sending a null character (`0x00`) to those sockets. Sultan can send approx. 1000 volleys/second, and more depending on how many threads and connections there are.

#### Syntax 
```
sultan xerxes "your_server" <port> <connections> <threads>
```

For example, to attack the malware site `gmil.com` on port 80 with 8 connections and 4 threads, run the following:
```
sultan xerxes "gmil.com" 80 8 4
```

Sultan will connect 8 sockets first and start the attack. After about 15 seconds, another thread will be created and *another 8 sockets allocated*. This process will be repeated until the maximum number of threads has been reached.

Note that for every thread, that many connections will be made. So basically,
**MAX_SOCKETS = CONNECTIONS * THREADS**.

### Slowloris
This attack replicates the infamous Slowloris attack that took down numerous Iranian websites in 2009.
It works by creating thousands of connections to the target server and keeping them alive for hours, or even days, occasionally sending `keep-alive` headers to prevent the victim from closing the connection.

#### Syntax
```
sultan slowloris "your_server" <port> <sockets> <ssl>
```

For example, to attack yourself on port 80 with 10,000 sockets with SSL, try
```
sultan slowloris localhost 80 10000 true
```
If the SSL is not provided or is not a valid Boolean value, Sultan defaults to FALSE.

### Ping of Death
#### Syntax
```
sultan deathping <host>
```
To attack a server called `ctepr`, try
```
sultan deathping "ctepr"
```

## Installation ![GitHub release](https://img.shields.io/github/release/lptstr/sultan.svg) ![GitHub All Releases](https://img.shields.io/github/downloads/lptstr/sultan/total.svg) 
Check the releases for the latest release, and download the appropriate `.zip` file for your platform. Then, add the `bin/Release/netcoreapp2.1/<platform>-<arch>/sultan(.exe)` file to your PATH.

## License
- MIT License


<br><br><br><br><br>
MIT (c) 2019 LPTSTR
