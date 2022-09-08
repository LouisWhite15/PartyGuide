import Equipment from "./equipment";

interface Game {
    id: string,
    name: string,
    description: string,
    requiredEquipment: Equipment[],
    rules: string
}

export default Game;