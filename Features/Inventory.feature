@inventory
Feature: Inventory
    As a logged-in user
    I want to browse and manage products
    So that I can add items to my cart

    Background:
        Given I am logged in as a standard user

    @smoke
    Scenario: Inventory page displays all products
        Then I should see 6 products on the inventory page

    @regression
    Scenario: Add product to cart
        When I add "Sauce Labs Backpack" to the cart
        Then the cart badge should show "1"

    @regression
    Scenario: Remove product from cart
        When I add "Sauce Labs Bike Light" to the cart
        And I remove "Sauce Labs Bike Light" from the cart
        Then the cart badge should show "0"
