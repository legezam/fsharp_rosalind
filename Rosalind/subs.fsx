#load "StringHelpers.fsx"
#load "IO.fsx"

let exercise = "subs"

open System
open System.Linq
open StringHelpers
open IO

let read = IO.ReadInputLines exercise
let t = read.[0] |> List.ofSeq
let s = read.[1] |> List.ofSeq

let result = StringHelper.findMatches t s 0
                |> List.map (fun item -> item + 1)
                |> (fun items -> String.Join(" ", items))

IO.WriteOutput exercise result

