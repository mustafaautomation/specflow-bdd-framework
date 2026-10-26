@cart
Feature: Shopping Cart
    As a logged-in user
    I want to manage items in my cart
    So that I can purchase products

    Background:
        Given I am logged in as a standard user
        And I am on the inventory page

    @smoke
    Scenario: Add single product to cart
        When I add "Sauce Labs Backpack" to the cart
        Then the cart badge should show "1"

    @regression
    Scenario: Add multiple products to cart
        When I add "Sauce Labs Backpack" to the cart
        And I add "Sauce Labs Bike Light" to the cart
        Then the cart badge should show "2"

    @regression
    Scenario: Remove product from cart
        When I add "Sauce Labs Bolt T-Shirt" to the cart
        And I remove "Sauce Labs Bolt T-Shirt" from the cart
        Then the cart badge should not be visible

    @regression
    Scenario: Cart displays correct items
        When I add "Sauce Labs Backpack" to the cart
        And I navigate to the cart
        Then the cart should contain "Sauce Labs Backpack"

    @regression
    Scenario: Proceed to checkout from cart
        When I add "Sauce Labs Backpack" to the cart
        And I navigate to the cart
        And I click checkout
        Then I should be on the checkout step one page
