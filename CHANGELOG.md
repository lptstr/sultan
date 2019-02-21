# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

### Unreleased
<details>
<summary>Features</summary>
<ul>
<li>Ability to control time waited between sending <code>keep-alive</code> headers in slowloris</li>
<li>An entirely new command-line interface with support for flags and options.</li> 
</ul>
</details>


### Orhan (v0.3.0) 2019-02-20
<details>
<summary>Features</summary>
<ul>
<li>Multithreading support for the <code>xerxes</code> attack</li>
<li>SSL support for <code>Slowloris</code> attack</li> 
</ul>
</details>

<details>
<summary>Changes</summary>
<ul>
<li>Add more pretty output.</li>
<li>Fix some rather arcane error messages.</li>
<li>Optimized some slow parts of Sultan</li>
<li>Handle all unhandled errors at the entrypoint, instead of allowing Sultan to just crash</li>
</ul>
</details>

<details>
<summary>Bug Fixes</summary>
<ul>
<li>Fixed issue #2 (All threads in XerXes attack use the same sockets)</li>
<li>Fixed issue #1 (System.ObjectDisposedException on XerXes attack)</li>
</ul>
</details>
<br>

### OSSMan 1 (v0.2.6) 2019-02-18
First release.
<details>
<summary>New Features</summary>
<ul>
<li>Slowloris attack</li>
<li>Attack form mimicking the so-called 'XerXes' attack</li>
<li>Ping of Death attack</li>
</ul>
</details>
