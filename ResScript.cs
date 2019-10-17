using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Res
{
    public List<Perms> perms = new List<Perms>();
    public List<Lines> lines = new List<Lines>();
}

[Serializable]
public class Perms
{
    public string inn;
    public string perm;

    public Perms(string _inn, string _perm)
    {
        this.inn = _inn;
        this.perm = _perm;
    }

    public Perms() { }
}

[Serializable]
public class Lines
{
    public string number;
    public string inn;
    public string place;
    public string targetDate;
    public string from;
    public string to;
    public string count;
    public string mass;
    public string v;
    public string getDate;
    public string sortCenter;
    public string fly;
    public string svhDate;
    public string sumbissionToCustoms;
    public string numDT;
    public string releaseFromCustoms;
    public string svhOut;
    public string delivery;
    public bool opened=false;
}