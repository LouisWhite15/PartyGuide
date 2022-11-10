import Match from "./match"
import Participant from "./participant"

export default interface Tournament {
    name : string,
    participants : Participant[]
    matches : Match[]
}
