# sultan [![Build status](https://ci.appveyor.com/api/projects/status/xhyyn53we4o20t3c?svg=true)](https://ci.appveyor.com/project/Kiedtl/sultan) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/41f5a32b2d63424c9681c62ab6cdb752)](https://www.codacy.com/app/lptstr/sultan?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=lptstr/sultan&amp;utm_campaign=Badge_Grade) ![GitHub](https://img.shields.io/github/license/lptstr/sultan.svg)

A tool to stress-test your server.
Sultan makes it easy to 'attack' your server to see how resiliant it would be should a malicious hacker try to DDOS your server.

Like this project? ![Say thanks!](https://img.shields.io/badge/Say%20Thanks-!-1EAEDB.svg)

## Usage
Sultan provides three attack methods as of v0.4.0: XerXes, SlowLoris, and DeathPing.

Basic syntax:
```bash
Usage: .\sultan slowloris [options]
Usage: .\sultan xerxes [options]
Usage: .\sultan deathping [options]
```

### XerXes
Ths attack mimicks the so-called XerXes DOS code online - by creating hundreds or thousands of sockets on the target server and repeatedly sending a null character (`0x00`) to those sockets. Sultan can send approx. 1000 volleys/second, and more depending on how many threads and connections there are.

#### Syntax 
```bash
sultan xerxes [options]
    -d, --host            <server>
    -p, --port            <port>
    -c, --connections     <connections>
    -t, --threads         <threads>
```

For example, to attack the malware site `gmil.com` on port 80 with 8 connections and 4 threads, run the following:
```bash
sultan xerxes -d 'gmil.com' -p 80 -t 4 -c 8
```

Sultan will connect 8 sockets first and start the attack. After about 15 seconds, another thread will be created and *another 8 sockets allocated*. This process will be repeated until the maximum number of threads has been reached.

Note that for every thread, that many connections will be made. So basically,
**MAX_SOCKETS = CONNECTIONS * THREADS**.

### Slowloris
This attack replicates the infamous Slowloris attack that took down numerous Iranian websites in 2009.
It works by creating thousands of connections to the target server and keeping them alive for hours, or even days, occasionally sending `keep-alive` headers to prevent the victim from closing the connection.

#### Syntax
```bash
sultan slowloris "your_server" <port> <sockets> <timeout> <ssl>
    -d, --host            <server>
    -p, --port            <port>
    -c, --connections     <connections>
    -t, --timeout         <timeout in ms.>
    -s, --ssl             <use ssl? true|false>
```

For example, to attack yourself on port 80 with 10,000 sockets with SSL and with a timeout of 120 s., try
```bash
sultan slowloris -d localhost -p 80 -s true -c 10000 -t 120000
```
If the SSL is not provided or is not a valid Boolean value, Sultan defaults to FALSE.

### Ping of Death
#### Syntax
```bash
sultan deathping [-d|--host] <host>
```
To attack a server called `ctepr`, try
```bash
sultan deathping --host "ctepr"
```

## Installation ![GitHub release](https://img.shields.io/github/release/lptstr/sultan.svg) ![GitHub All Releases](https://img.shields.io/github/downloads/lptstr/sultan/total.svg) 

<details>
    <summary>macOS / Linux</summary>
    <ul>
        <li>
            Try using <a href="https://scoop.sh">Scoop.</a><br>
            <div class="highlight highlight-source-shell">
                <pre>
                    scoop bucket add lptstr https://github.com/lptstr/lptstr-scoop
                    scoop install sultan
                </pre>
            </div>
        </li>
    </ul>
</details>

<details>
    <summary>Windows</summary>
    <ul>
        <li>
            Check the releases for the latest release, and download the appropriate <code>.zip</code> file for your platform. Then, add the <code>bin/Release/netcoreapp2.1/<platform>-<arch>/sultan(.exe)</code> file to your PATH.
        </li>
    </ul>
</details>

## License
  - MIT License

<br>

[![forthebadge](https://forthebadge.com/images/badges/built-by-developers.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/powered-by-electricity.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/gluten-free.svg)](https://forthebadge.com)
