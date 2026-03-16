Feature: Academics registration

Scenario: Registering a academic makes the read model available
	Given the Academics application composition is configured
	When I register a academic named "Alice" "Bennett"
	Then the registered academic can be retrieved from the query side
