#load "StringHelpers.fsx"

let input = "/tmp/rosalind_subs.txt"
let output = "/tmp/rosalind_subsresult.txt"

open System.IO
open System
open System.Linq
open StringHelpers

let read = File.ReadAllLines(input)
let t = read.[0] |> List.ofSeq
let s = read.[1] |> List.ofSeq

let result = StringHelper.findMatches t s 0
                |> List.map (fun item -> item + 1)
                |> (fun items -> String.Join(" ", items))

File.WriteAllText(output, sprintf "%s" result)
