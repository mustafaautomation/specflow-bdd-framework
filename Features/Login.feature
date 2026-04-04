@login
Feature: Login
    As a user
    I want to log in to SauceDemo
    So that I can access the inventory

    Background:
        Given I am on the login page

    @smoke
    Scenario: Successful login with standard user
        When I login with "standard_user" and "secret_sauce"
        Then I should be on the inventory page

    @smoke
    Scenario: Login fails with locked out user
        When I login with "locked_out_user" and "secret_sauce"
        Then I should see the error "this user has been locked out"

    @smoke
    Scenario: Login fails with empty username
        When I login with "" and "secret_sauce"
        Then I should see the error "Username is required"

    @smoke
    Scenario: Login fails with empty password
        When I login with "standard_user" and ""
        Then I should see the error "Password is required"

    @regression
    Scenario Outline: Login fails with invalid credentials
        When I login with "<username>" and "<password>"
        Then I should see the error "do not match"

        Examples:
            | username     | password   |
            | bad_user     | bad_pass   |
            | invalid      | wrong      |
