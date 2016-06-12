#load "StringHelpers.fsx"
#load "IO.fsx"

let exercise = "revc"

open System
open System.Linq
open StringHelpers
open IO

let swapAT = StringHelper.swap 'A' 'T'
let swapCG = StringHelper.swap 'C' 'G'

let result = IO.ReadInput exercise
                |> Seq.rev
                |> Seq.map (fun item -> swapAT (swapCG item))
                |> (fun chars -> String.Join("", chars))

IO.WriteOutput exercise result

