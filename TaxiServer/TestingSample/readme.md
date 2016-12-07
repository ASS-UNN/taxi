## Controller
Handles remote method access requests from client instances via scs.
Provides indirect access to core functionality through *Ctrl classes' methods.
Example: OrderCreationCtrl (controller that can validate user input and create an order instance)

## Model
Represents structures and classes that provide core functionality PER CLIENT instance. 
Example: OrderInstanceModel (a singular order instance for some particular client)

## Base
Represents classes that provide business logic functionality for ALL CLIENTs concurrently.
Example: OrderPoolModel (a pool of pending orders for all clients throughout the entire system)