EXTRA_DIST =  
COMPILE_TARGET = exe 
BUILD_DIR=$(top_builddir)/bin
ASSEMBLY = $(BUILD_DIR)/udetect.exe
PROJECT_REFERENCES = $(BUILD_DIR)/Ude.dll

if ENABLE_DEBUG_ANY_CPU
ASSEMBLY_COMPILER_FLAGS = -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;TRACE"
endif

if ENABLE_RELEASE_ANY_CPU
ASSEMBLY_COMPILER_FLAGS = -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"
endif

EXTRAS = udetect.in 
REFERENCES = System 

CLEANFILES = $(BINARIES) 
MAINTAINERCLEANFILES = Makefile.in

FILES = Ude.Example/Udetect.cs Properties/AssemblyInfo.cs 

include $(top_srcdir)/Makefile.include

UDE_DLL = $(BUILD_DIR)/Ude.dll
BINARIES = $(UDETECT)

all: $(ASSEMBLY) $(BINARIES) 

UDETECT = $(BUILD_DIR)/udetect

$(eval $(call emit-deploy-wrapper,UDETECT,udetect, x))

$(ASSEMBLY): $(build_sources) $(PROJECT_REFERENCES) 
	mkdir -p $(dir $(ASSEMBLY))
	gmcs $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_references_ref)
	
	
