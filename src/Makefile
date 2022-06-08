VERSION ?= $(shell git describe --tags --abbrev=0)
CSPROJ_PATH ?= ./PoeFilterX/PoeFilterX.csproj
RELEASES_PATH ?= ./Releases/$(VERSION)
TEST_PATH ?= $(RELEASES_PATH)/Test
TEST_FILTER ?= $(TEST_PATH)/TestFilter.filterx
TEST_ENVS ?= $(TEST_PATH)/TieredCurrencies.env
DEBUG_TYPE ?= None
VERBOSITY ?= quiet
FRAMEWORK ?= net6.0
SINGLE_FILE ?= true
DEBUG_SYMBOLS ?= false
FLAVOR ?= linux-x64
SELF_CONTAINED ?= true
CONFIGURATION ?= Release

.PHONY: build
build: $(RELEASES_PATH)
$(RELEASES_PATH):
	dotnet publish "$(CSPROJ_PATH)" -c $(CONFIGURATION) -r $(FLAVOR) --self-contained $(SELF_CONTAINED) -o "$(RELEASES_PATH)" /p:Version="$(VERSION)-$(FLAVOR)" /p:DebugType=$(DEBUG_TYPE) /p:DebugSymbols=$(DEBUG_SYMBOLS) /p:PublishSingleFile=$(SINGLE_FILE) -f $(FRAMEWORK) --verbosity $(VERBOSITY)

.PHONY: clean
clean:
	rm -rf $(RELEASES_PATH)

test:
	env -v $(shell grep -v '^#' $(TEST_ENVS) | xargs -d '\n') $(RELEASES_PATH)/PoeFilterX build --p $(TEST_FILTER) --v true
