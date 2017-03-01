namespace Library1

module Tetris = 

    type Tetromino =  I | J | L | O | S | T | Z

    type Tile = 
        | Empty
        | Wall
        | Filled of Tetromino 
        
    type Pos = int * int

    type Shape = {
        name: Tetromino 
        coords: Pos*Pos*Pos*Pos
        pos: Pos
    }

    type Status = Ready | Playing | Paused | Dead

    type GameState = {
        CurrentShape:Shape //option
        Board:Tile[,]
        HighScore: int
        Status: Status
    }

    let startPos = (4,2)

    let InitalShapes = [
        {name = I; coords=(0, -2), (0, -1), (0, 0), (0, 1); pos=startPos}
        {name = J; coords=(0, 1), (0, 0), (0, -1), (-1, -1); pos=startPos}
        {name = L; coords=(0, 1), (0, 0), (0, -1), (1, -1); pos=startPos}
        {name = O; coords=(0, 0), (0, 1), (1, 0), (1, 1); pos=startPos}
        {name = S; coords=(-1, 0), (0, 0), (0, 1), (1, 1); pos=startPos}
        {name = T; coords=(-1, 0), (0, 0), (1, 0), (0, 1); pos=startPos}
        {name = Z; coords=(-1, 1), (0, 1), (0, 0), (1, 0); pos=startPos}
    ]

    type Action = 
        | NoAction
        | Rotate
        | Move of int
        | Drop

    let rand = System.Random();

    let SelectRandomShape () = 
        let index = rand.Next(InitalShapes.Length)
        List.item index InitalShapes
        
    let rotate shape = 
        let r (x,y) = (y,-x)
        let a,b,c,d = shape.coords
        {shape with coords = r a, r b, r c, r d}

    let left (x,y) = (x-1,y)
    let right (x,y) = (x+1,y)
    let down (x,y) = (x,y+1)

    let move dir shape = {shape with pos= dir shape.pos}

    let getActualCoords (x1,y1) (a,b,c,d) =
        let f (x2,y2) = x1+x2,y1+y2 
        f a, f b, f c, f d             

    let IsValidPos (board:Tile[,]) shape =  // TODO check if outofbounds
        let f (x,y) =
            match board.[x,y] with
            | Empty -> true
            | Wall -> false
            | Filled _ -> false
        let a,b,c,d = getActualCoords shape.pos shape.coords
        f a && f b && f c && f d
                 
    let CanRotate board shape = 
        let movedShape = rotate shape
        IsValidPos board movedShape

    let CanMove board shape dir = 
        let movedShape = move dir shape
        IsValidPos board movedShape

    let AddShapeToBoard board shape = 
        let mapping x y tile = 
            let a,b,c,d = getActualCoords shape.pos shape.coords
            match (x,y) with
            | t when (t = a || t=b|| t=c|| t=d) -> Tile.Filled shape.name
            | _ -> tile
        Array2D.mapi mapping board
        
        
    let UpdateCurrentShape (gameState:GameState) = 
        let newBoard = AddShapeToBoard gameState.Board gameState.CurrentShape
        {gameState with Board = newBoard; CurrentShape = SelectRandomShape ()}

    let DoMoveDown gameState =                     
        match CanMove gameState.Board gameState.CurrentShape down with
        | true -> {gameState with CurrentShape=move down gameState.CurrentShape}
        | false -> UpdateCurrentShape gameState

    let DoMove gameState dir = 
        match CanMove gameState.Board gameState.CurrentShape dir with
        | true -> {gameState with CurrentShape=move dir gameState.CurrentShape}
        | false -> gameState

    let DoMoveLeft gameState = DoMove gameState left
    let DoMoveRight gameState = DoMove gameState right

    let DoRotate gameState = 
        match CanRotate gameState.Board gameState.CurrentShape with
        | true -> {gameState with CurrentShape=rotate gameState.CurrentShape}
        | false -> gameState
        

    let GameTick action gameState = 
        // apply action
        // 
        ()
        
    let gameDimensions = 20,30

    let NewGameState =   
        let dimX, dimY = gameDimensions
        let board = Array2D.init dimX dimY (fun x y -> 
            match x,y with
            | _,0  -> Wall
            | 0,_  -> Wall
            | _ when x = dimX-1 -> Wall
            | _ when y = dimY-1 -> Wall
            | _ -> Empty            
        )
        {CurrentShape=SelectRandomShape (); Board = board; HighScore = 0; Status = Playing}

    ()

