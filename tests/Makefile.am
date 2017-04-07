if ENABLE_TESTS

EXTRA_DIST = 
COMPILE_TARGET = library
BUILD_DIR=bin/Debug
ASSEMBLY = $(BUILD_DIR)/Ude.Tests.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
PROJECT_REFERENCES = $(top_builddir)/bin/Ude.dll
ASSEMBLY_COMPILER_FLAGS = -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;TRACE"

FILES = \
	Properties/AssemblyInfo.cs \
	Ude.Tests/BitPackageTest.cs \
	Ude.Tests/CharsetDetectorTest.cs \
	Ude.Tests/CharsetProberTest.cs \
	Ude.Tests/CharsetDetectorTestBatch.cs 

EXTRAS = \
	Data/big5/1.txt \
	Data/eucjp/1.txt \
	Data/euckr/euc1.txt \
	Data/euckr/euc2.txt \
	Data/gb18030/1.txt \
	Data/ibm855/1.txt \
	Data/ibm866/1.txt \
	Data/iso2022jp/1.txt \
	Data/iso2022kr/iso1.txt \
	Data/iso2022kr/iso2.txt \
	Data/iso88597/greek.txt \
	Data/koi8r/1.txt \
	Data/latin1/1.txt \
	Data/latin1/2.txt \
	Data/latin1/3.txt \
	Data/latin1/4.txt \
	Data/maccyrillic/1.txt \
	Data/shiftjis/1.txt \
	Data/shiftjis/2.txt \
	Data/shiftjis/3.txt \
	Data/shiftjis/4.txt \
	Data/utf8/1.txt \
	Data/utf8/2.txt \
	Data/utf8/3.txt \
	Data/utf8/4.txt \
	Data/utf8/5.txt \
	Data/utf8/greek.txt \
	Data/utf8/he1.txt \
	Data/utf8/he2.txt \
	Data/utf8/he3.txt \
	Data/utf8/russian.txt \
	Data/windows1251/1.txt \
	Data/windows1253/greek.txt \
	Data/windows1255/he1.txt \
	Data/windows1255/he2.txt \
	Data/windows1255/he3.txt 

REFERENCES = System $(MONO_NUNIT_LIBS) 
PROGRAMFILES = $(UDE_DLL)   

CLEANFILES = $(PROGRAMFILES) $(BUILD_DIR)/* 
MAINTAINERCLEANFILES = Makefile.in

all: $(ASSEMBLY) $(PROGRAMFILES) 

test: $(ASSEMBLY)
	nunit-console $(ASSEMBLY) 

include $(top_srcdir)/Makefile.include

UDE_DLL = $(top_builddir)/bin/Ude.dll

$(eval $(call emit-deploy-target,UDE_DLL))

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) 
	mkdir -p $(dir $(ASSEMBLY))
	cp $(PROGRAMFILES) $(dir $(ASSEMBLY))
	gmcs $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_references_ref)

endif

