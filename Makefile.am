SUBDIRS =  src/Library src/Example src/Tests m4

clean-local:
	rm -rf $(top_builddir)/bin

if ENABLE_TESTS
test: 
	@pushd src/Tests; \
	make test; \
	popd;	
endif

MAINTAINERCLEANFILES = Makefile.in
